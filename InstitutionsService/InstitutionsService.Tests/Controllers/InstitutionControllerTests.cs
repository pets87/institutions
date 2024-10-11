using InstitutionsService.Controllers;
using InstitutionsService.Models;
using InstitutionsService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InstitutionsService.Tests.Controllers
{
    [TestClass]
    public class InstitutionControllerTests
    {
        private Mock<IInstitutionService> mockInstitutionService;
        private Mock<ILogger<InstitutionController>> mockLogger;
        private InstitutionController institutionController;

        [TestInitialize]
        public void Setup()
        {
            mockInstitutionService = new Mock<IInstitutionService>();
            mockLogger = new Mock<ILogger<InstitutionController>>();
            institutionController = new InstitutionController(mockInstitutionService.Object, mockLogger.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsOk_WithInstitutions()
        {
            var sampleInstitutions = new List<Institution>
            {
                new Institution { Id = 1, Name = "Institution 1" },
                new Institution { Id = 2, Name = "Institution 2" }
            };
            mockInstitutionService.Setup(s => s.GetInstitutionsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(sampleInstitutions);

            var result = await institutionController.Get(0, 10);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
            var returnedInstitutions = okResult.Value as IEnumerable<Institution>;
            Assert.IsNotNull(returnedInstitutions);
            Assert.AreEqual(2, returnedInstitutions.Count());
        }

        [TestMethod]
        public async Task GetById_ReturnsOk_WhenInstitutionExists()
        {
            var institution = new Institution { Id = 1, Name = "Institution 1" };
            mockInstitutionService.Setup(s => s.GetInstitutionAsync(1)).ReturnsAsync(institution);

            var result = await institutionController.GetById(1);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(institution, okResult.Value);
        }

        [TestMethod]
        public async Task GetById_ReturnsNotFound_WhenInstitutionDoesNotExist()
        {
            mockInstitutionService.Setup(s => s.GetInstitutionAsync(It.IsAny<long>())).ReturnsAsync((Institution)null);

            var result = await institutionController.GetById(999);

            var notFoundResult = result.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsOk_WhenSuccessful()
        {
            mockInstitutionService.Setup(s => s.Delete(It.IsAny<long>())).ReturnsAsync(true);

            var result = await institutionController.Delete(1);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

        [TestMethod]
        public async Task Put_ReturnsOk_WhenUpdateSuccessful()
        {
            var institutionToUpdate = new Institution { Id = 1, Name = "Updated Institution" };
            mockInstitutionService.Setup(s => s.Update(1, institutionToUpdate)).ReturnsAsync(institutionToUpdate);

            var result = await institutionController.Put(1, institutionToUpdate);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(institutionToUpdate, okResult.Value);
        }

        [TestMethod]
        public async Task Post_ReturnsCreated_WhenInstitutionCreated()
        {
            var newInstitution = new Institution { Id = 1, Name = "New Institution" };
            mockInstitutionService.Setup(s => s.Insert(It.IsAny<Institution>())).ReturnsAsync(newInstitution);

            var result = await institutionController.Post(newInstitution);

            var createdResult = result.Result as CreatedResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(newInstitution, createdResult.Value);
        }

        [TestMethod]
        public async Task Get_Returns500_OnException()
        {
            mockInstitutionService.Setup(s => s.GetInstitutionsAsync(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));

            var result = await institutionController.Get(0, 10);

            var objectResult = result.Result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

        [TestMethod]
        public async Task Delete_Returns500_OnException()
        {
            mockInstitutionService.Setup(s => s.Delete(It.IsAny<long>())).ThrowsAsync(new Exception("Test exception"));

            var result = await institutionController.Delete(1);

            var objectResult = result.Result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }
    }
}
