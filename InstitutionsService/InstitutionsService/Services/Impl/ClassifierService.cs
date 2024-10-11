using InstitutionsService.Data;
using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Services.Impl
{
    public class ClassifierService : IClassifierService
    {
        private readonly ApplicationDbContext context;
        public ClassifierService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Classifier>> GetAllClassifiers()
        {
            return await context.Classifiers.ToListAsync();
        }

        public async Task<IEnumerable<Classifier>> GetClassifiersByGroups(List<string> groups)
        {
            return await context.Classifiers.Where(x => groups.Contains(x.Group)).ToListAsync();
        }

        public async Task<IEnumerable<Classifier>> GetClassifiersByIds(List<long> ids)
        {
            return await context.Classifiers.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
