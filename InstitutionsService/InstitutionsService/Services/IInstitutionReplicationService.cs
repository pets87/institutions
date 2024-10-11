using InstitutionsService.Models;

namespace InstitutionsService.Services
{
    public interface IInstitutionReplicationService
    {
        Task<List<InstitutionReplication>> BulkInsert(List<InstitutionReplication> institutionReplications);
    }
}
