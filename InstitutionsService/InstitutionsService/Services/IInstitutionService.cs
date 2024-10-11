using InstitutionsService.Models;

namespace InstitutionsService.Services
{
    public interface IInstitutionService
    {
        Task<IEnumerable<Institution>> GetInstitutionsAsync(int offset = 0, int limit = 10);
        Task<Institution> GetInstitutionAsync(long id);
        Task<IEnumerable<Institution>> GetInstitutionsByIdsAsync(List<long> ids);
        Task<Institution> Update(long id, Institution institution);
        Task<Institution> Insert(Institution institution);
        Task<bool> Delete(long id);
    }
}
