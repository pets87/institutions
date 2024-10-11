using InstitutionsService.Data;
using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Services.Impl
{
    public class InstitutionService : IInstitutionService
    {
        private readonly ApplicationDbContext context;
        public InstitutionService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Institution>> GetInstitutionsAsync(int offset = 0, int limit = 10)
        {
            var result = await context.Institutions
                .Include(x => x.Replications)
                .Include(x => x.Address)
                .Where(x => x.DeletedDateTime == null)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            await LoadTranslations(result);

            return result.OrderBy(x => x.Name);
        }
        public async Task<Institution> GetInstitutionAsync(long id)
        {
            var result = await context.Institutions
                .Include(x => x.Replications)
                .Include(x => x.Address)
                .Where(x => x.DeletedDateTime == null)
                .FirstOrDefaultAsync(i => i.Id == id);

            await LoadTranslations(new List<Institution>() { result });

            return result;
        }

        public async Task<IEnumerable<Institution>> GetInstitutionsByIdsAsync(List<long> ids)
        {
            var result = await context.Institutions.AsNoTracking()
                .Include(x => x.Address)
                .Where(x => x.DeletedDateTime == null && ids.Contains(x.Id)).ToListAsync();

            await LoadTranslations(result);

            return result;
        }

        private async Task LoadTranslations(List<Institution> result)
        {
            //Replications and addresses are loaded automatically, but translations must be loaded separately, because they are found by code, not id.
            var translationCodes = result.Where(x => !string.IsNullOrWhiteSpace(x.NameTranslationCode)).Select(x => x.NameTranslationCode).ToHashSet().ToList();
            var translations = await context.Translations.Where(x => translationCodes.Contains(x.Code)).ToListAsync();
            foreach (var row in result)
            {
                row.Translations = translations.Where(x => x.Code == row.NameTranslationCode).ToList();
            }

        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var institution = await GetInstitutionAsync(id);

                if (institution == null)
                    return false;

                institution.DeletedDateTime = DateTime.UtcNow;

                if (institution.Translations != null)
                {
                    foreach (var translation in institution.Translations)
                    {
                        translation.DeletedDateTime = DateTime.UtcNow;
                    }
                }

                if (institution.Replications != null)
                {
                    foreach (var replication in institution.Replications)
                    {
                        replication.DeletedDateTime = DateTime.UtcNow;
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<Institution> Update(long id, Institution institution)
        {
            if (id != institution.Id)
            {
                throw new ArgumentException("ID in the URL does not match ID in the institution object");
            }

            context.Institutions.Update(institution);
            await context.SaveChangesAsync();
            return institution;
        }

        public async Task<Institution> Insert(Institution institution)
        {
            if (institution.Id > 0)
            {
                throw new ArgumentException("New institution object cannot have ID > 0. For update use PUT instead");
            }
            context.Institutions.Add(institution);
            await context.SaveChangesAsync();

            return institution;
        }
    }
}
