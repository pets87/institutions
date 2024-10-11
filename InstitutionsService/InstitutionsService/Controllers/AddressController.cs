using InstitutionsService.Models;
using InstitutionsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService addressService;
        private readonly ILogger<AddressController> logger;
        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            this.addressService = addressService;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> SearchAddress(string text)
        {
            try
            {
                return Ok(await addressService.SearchAddress(text));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Address.SearchAddress.Error");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
