using InstitutionsService.Controllers;
using InstitutionsService.Models;
using InstitutionsService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InstitutionsService.Tests.Controllers
{
    [TestClass]
    public class ClassifierControllerTests
    {
        private Mock<IClassifierService> mockClassifierService;
        private Mock<ILogger<ClassifierController>> mockLogger;
        private ClassifierController classifierController;

        [TestInitialize]
        public void Setup()
        {
            mockClassifierService = new Mock<IClassifierService>();
            mockLogger = new Mock<ILogger<ClassifierController>>();
            classifierController = new ClassifierController(mockClassifierService.Object, mockLogger.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsOk_WithClassifiers()
        {
            var sampleClassifiers = new List<Classifier>
            {
                new Classifier { Id = 1, Name = "Sample Classifier 1" },
                new Classifier { Id = 2, Name = "Sample Classifier 2" }
            };
            mockClassifierService.Setup(s => s.GetAllClassifiers()).ReturnsAsync(sampleClassifiers);

            var result = await classifierController.Get();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
            var returnedClassifiers = okResult.Value as IEnumerable<Classifier>;
            Assert.IsNotNull(returnedClassifiers);
            Assert.AreEqual(2, returnedClassifiers.Count());
        }

        [TestMethod]
        public async Task Get_Returns500_OnException()
        {
            mockClassifierService.Setup(s => s.GetAllClassifiers()).ThrowsAsync(new Exception("Test exception"));

            var result = await classifierController.Get();

            var objectResult = result.Result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsOk_WithEmptyList_WhenNoClassifiersFound()
        {
            mockClassifierService.Setup(s => s.GetAllClassifiers()).ReturnsAsync(new List<Classifier>());

            var result = await classifierController.Get();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedClassifiers = okResult.Value as IEnumerable<Classifier>;
            Assert.IsNotNull(returnedClassifiers);
            Assert.AreEqual(0, returnedClassifiers.Count());
        }
    }
}
