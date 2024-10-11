using InstitutionsService.Models;

namespace InstitutionsService.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> SearchAddress(string text);
    }
}
