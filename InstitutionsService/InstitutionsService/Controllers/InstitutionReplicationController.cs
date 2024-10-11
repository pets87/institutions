using InstitutionsService.Models;
using InstitutionsService.Services;
using InstitutionsService.Validators.InstitutionReplication;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionsService.Controllers
{
    [ApiController]
    [Route("")]
    public class InstitutionReplicationController : ControllerBase
    {
        private readonly IInstitutionReplicationService institutionReplicationService;
        private readonly ILogger<InstitutionReplicationController> logger;
        public InstitutionReplicationController(IInstitutionReplicationService institutionReplicationService, ILogger<InstitutionReplicationController> logger)
        {
            this.institutionReplicationService = institutionReplicationService;
            this.logger = logger;
        }

        [HttpPost("InstitutionReplications")]
        public async Task<ActionResult<InstitutionReplication>> Post([InstitutionReplicationCreateValidator][FromBody] List<InstitutionReplication> institutionReplications)
        {
            try
            {
                return Created("InstitutionReplications", await institutionReplicationService.BulkInsert(institutionReplications));
            }
            catch (Exception e)
            {
                logger.LogError(e, "InstitutionReplications.Create.Error");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
