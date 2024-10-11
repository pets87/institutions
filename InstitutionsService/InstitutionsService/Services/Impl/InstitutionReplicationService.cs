using InstitutionsService.Data;
using InstitutionsService.Models;
using InstitutionsService.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMQ.Client;
using System.Text.Json.Serialization;
using System.Text.Json;
using InstitutionsService.RabbitMQ.Dto;

namespace InstitutionsService.Services.Impl
{
    public class InstitutionReplicationService : IInstitutionReplicationService
    {
        private readonly IInstitutionService institutionService;
        private readonly IClassifierService classifierService;
        private readonly IRabbitMqClient rabbitMqClient;
        private readonly ApplicationDbContext context;
        public InstitutionReplicationService(ApplicationDbContext context, IInstitutionService institutionService, IClassifierService classifierService, IRabbitMqClient rabbitMqClient)
        {
            this.context = context;
            this.rabbitMqClient = rabbitMqClient;
            this.classifierService = classifierService;
            this.institutionService = institutionService;
        }
        public async Task<List<InstitutionReplication>> BulkInsert(List<InstitutionReplication> institutionReplications)
        {
            this.context.InstitutionReplications.AddRange(institutionReplications);

            await Publish(institutionReplications.Where(x=>x.PlannedPublishDateTime == null).ToList());

            await this.context.SaveChangesAsync(); // save changes after publishing. Publish datetime is set on saving inside InstitutionReplicationInterceptor

            return institutionReplications;
        }

        private async Task Publish(List<InstitutionReplication> institutionReplications)
        {
            if (!institutionReplications.Any())
                return;

            var institutionIds = institutionReplications.Select(x => x.InstitutionId).ToHashSet().ToList();            
            var institutions = await institutionService.GetInstitutionsByIdsAsync(institutionIds);

            var institutionTypeIds = institutions.Select(x=>x.TypeClassifierId).ToHashSet();
            var systemIds = institutionReplications.Select(x => x.SystemClassifierId).ToHashSet();
            var environmentIds = institutionReplications.Select(x => x.EnvironmentClassifierId).ToHashSet();
            var classifierIds = systemIds.Union(environmentIds).Union(institutionTypeIds).ToList();
            var classifiers = await classifierService.GetClassifiersByIds(classifierIds);

            foreach (var institutionReplication in institutionReplications)
            {
                var system = classifiers.FirstOrDefault(x => x.Id == institutionReplication.SystemClassifierId);
                var environment = classifiers.FirstOrDefault(x => x.Id == institutionReplication.EnvironmentClassifierId);
                var institution = institutions.ToList().Find(x => x.Id == institutionReplication.InstitutionId);
                if (system == null || environment == null || institution == null)
                    continue;

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,  
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    WriteIndented = false // No pretty printing
                };

                var dto = ConvertToInstitutionDto(institution, classifiers.ToList());
                var data = System.Text.Json.JsonSerializer.Serialize(dto, options);
                rabbitMqClient.Publish(system.Name, environment.Name, data);
            }
        }
        
        public static InstitutionDto ConvertToInstitutionDto(Institution institution, List<Classifier> classifiers)
        {
            return new InstitutionDto()
            {
                Id = institution.Id,
                Name = institution.Name,
                RegCode = institution.RegCode,
                KMKR = institution.KMKR,
                Type = classifiers.Find(x=>x.Id == institution.TypeClassifierId),
                ValidFrom = institution.ValidFrom,
                ValidTo = institution.ValidTo,
                Address = institution.Address,
                Translations = institution.Translations
            };
        }

    }
}
