using Moq;
using InstitutionsService.Controllers;
using InstitutionsService.Models;
using InstitutionsService.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionsService.Tests.Controllers
{
    [TestClass]
    public class InstitutionReplicationControllerTests
    {
        private Mock<IInstitutionReplicationService> mockInstitutionReplicationService;
        private Mock<ILogger<InstitutionReplicationController>> mockLogger;
        private InstitutionReplicationController institutionReplicationController;

        [TestInitialize]
        public void TestInitialize()
        {
            mockInstitutionReplicationService = new Mock<IInstitutionReplicationService>();
            mockLogger = new Mock<ILogger<InstitutionReplicationController>>();
            institutionReplicationController = new InstitutionReplicationController(mockInstitutionReplicationService.Object, mockLogger.Object);
        }

        [TestMethod]
        public async Task Post_ReturnsCreated_WhenInstitutionReplicationsAreInsertedSuccessfully()
        {
            var institutionReplications = new List<InstitutionReplication>
            {
                new InstitutionReplication { Id = 1, InstitutionId = 1, SystemClassifierId = 2 },
                new InstitutionReplication { Id = 2, InstitutionId = 1, SystemClassifierId = 2 }
            };

            mockInstitutionReplicationService
                .Setup(s => s.BulkInsert(It.IsAny<List<InstitutionReplication>>()))
                .ReturnsAsync(institutionReplications);

            var result = await institutionReplicationController.Post(institutionReplications);

            var createdResult = result.Result as CreatedResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual("InstitutionReplications", createdResult.Location);
            Assert.AreEqual(institutionReplications, createdResult.Value);
        }

        [TestMethod]
        public async Task Post_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            var institutionReplications = new List<InstitutionReplication>
            {
                new InstitutionReplication { Id = 1, InstitutionId = 1, SystemClassifierId = 3 }
            };

            mockInstitutionReplicationService
                .Setup(s => s.BulkInsert(It.IsAny<List<InstitutionReplication>>()))
                .ThrowsAsync(new System.Exception("Test exception"));

            var result = await institutionReplicationController.Post(institutionReplications);

            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error", statusCodeResult.Value);
        }
    }
}
