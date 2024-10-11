using InstitutionsService.Data;
using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Services.Impl
{
    public class TranslationService : ITranslationService
    {
        private readonly ApplicationDbContext context;
        public TranslationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Translation>> GetAllTranslations()
        {
            return await context.Translations.ToListAsync();
        }
        public async Task<Translation> Update(long id, Translation translation)
        {
            if (id != translation.Id)
            {
                throw new ArgumentException("ID in the URL does not match ID in the translation object");
            }

            context.Translations.Update(translation);
            await context.SaveChangesAsync();
            return translation;
        }
    }
}
