using InstitutionsService.Models;

namespace InstitutionsService.Services
{
    public interface ITranslationService
    {
        Task<IEnumerable<Translation>> GetAllTranslations();
        Task<Translation> Update(long id, Translation translation);
    }
}
