using InstitutionsService.Models;
using InstitutionsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassifierController : ControllerBase
    {
        private readonly IClassifierService classifierService; 
        private readonly ILogger<ClassifierController> logger;
        public ClassifierController(IClassifierService classifierService, ILogger<ClassifierController> logger)
        {
            this.classifierService = classifierService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classifier>>> Get() 
        {
            try
            {
                return Ok(await classifierService.GetAllClassifiers());
            }
            catch (Exception e) 
            {
                logger.LogError(e, "Classifier.Get.Error");
                return StatusCode(500, "Internal server error");
            }           
        }
    }
}
