using InstitutionsService.Models;

namespace InstitutionsService.Services
{
    public interface IClassifierService
    {
        Task<IEnumerable<Classifier>> GetAllClassifiers();
        Task<IEnumerable<Classifier>> GetClassifiersByGroups(List<string> groups);
        Task<IEnumerable<Classifier>> GetClassifiersByIds(List<long> ids);
    }
}
