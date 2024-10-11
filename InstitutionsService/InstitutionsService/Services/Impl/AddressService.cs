using InstitutionsService.Data;
using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Services.Impl
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext context;
        public AddressService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Address>> SearchAddress(string text)
        {
            if (text.Length < 3)
                return new List<Address>();
            return await context.Addresses.Where(x=>x.AddressText.ToLower().Contains(text.ToLower())).ToListAsync();
        }
    }
}
