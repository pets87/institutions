using InstitutionsService.Models;
using InstitutionsService.Services;
using InstitutionsService.Validators.Translation;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService translationService;
        private readonly ILogger<TranslationController> logger;
        public TranslationController(ITranslationService translationService, ILogger<TranslationController> logger)
        {
            this.translationService = translationService;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Translation>>> Get()
        {
            try
            {
                return Ok(await translationService.GetAllTranslations());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Translation.Get.Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Translation>> Put([FromRoute]long id, [TranslationUpdateValidator][FromBody] Translation translation) 
        {
            try
            {
                return Ok(await translationService.Update(id, translation));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Translation.Update.Error");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
