using InstitutionsService.Models;
using InstitutionsService.Services;
using InstitutionsService.Validators.Institution;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService institutionService;
        private readonly ILogger<InstitutionController> logger;
        public InstitutionController(IInstitutionService institutionService, ILogger<InstitutionController> logger)
        {
            this.institutionService = institutionService;
            this.logger = logger;            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institution>>> Get(int offset = 0, int limit = 10)
        {
            try
            {
                return Ok(await institutionService.GetInstitutionsAsync(offset, limit));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Institution.Get.Error");
                return StatusCode(500, "Internal server error");
            }           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Institution>> GetById([FromRoute] long id)
        {
            try
            {
                var institution = await institutionService.GetInstitutionAsync(id);
                if (institution == null)
                    return NotFound();
                return Ok(institution);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Institution.GetById.Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] long id)
        {
            try
            {
                return Ok(await institutionService.Delete(id));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Institution.Delete.Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Institution>> Put([FromRoute] long id, [InstitutionUpdateValidator][FromBody] Institution institution)
        {
            try
            {
                return Ok(await institutionService.Update(id, institution));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Institution.Update.Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<Institution>> Post([InstitutionCreateValidator][FromBody] Institution institution)
        {
            try
            {
                return Created(nameof(Institution), await institutionService.Insert(institution));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Institution.Create.Error");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
