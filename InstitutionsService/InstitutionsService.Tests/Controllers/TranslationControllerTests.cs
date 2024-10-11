using Moq;
using InstitutionsService.Controllers;
using InstitutionsService.Models;
using InstitutionsService.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionsService.Tests.Controllers
{
    [TestClass]
    public class TranslationControllerTests
    {
        private Mock<ITranslationService> mockTranslationService;
        private Mock<ILogger<TranslationController>> mockLogger;
        private TranslationController translationController;

        [TestInitialize]
        public void TestInitialize()
        {
            mockTranslationService = new Mock<ITranslationService>();
            mockLogger = new Mock<ILogger<TranslationController>>();
            translationController = new TranslationController(mockTranslationService.Object, mockLogger.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsOk_WhenTranslationsExist()
        {
            var translations = new List<Translation>
            {
                new Translation { Id = 1, Code = "Translation1" },
                new Translation { Id = 2, Code = "Translation2" }
            };

            mockTranslationService.Setup(s => s.GetAllTranslations()).ReturnsAsync(translations);

            var result = await translationController.Get();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(translations, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            mockTranslationService.Setup(s => s.GetAllTranslations()).ThrowsAsync(new System.Exception("Test exception"));

            var result = await translationController.Get();

            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error", statusCodeResult.Value);
        }

        [TestMethod]
        public async Task Put_ReturnsOk_WhenTranslationIsUpdatedSuccessfully()
        {
            var translation = new Translation { Id = 1, Code = "UpdatedTranslation" };

            mockTranslationService.Setup(s => s.Update(It.IsAny<long>(), It.IsAny<Translation>())).ReturnsAsync(translation);

            var result = await translationController.Put(1, translation);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(translation, okResult.Value);
        }

        [TestMethod]
        public async Task Put_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            var translation = new Translation { Id = 1, Code = "UpdatedTranslation" };

            mockTranslationService.Setup(s => s.Update(It.IsAny<long>(), It.IsAny<Translation>())).ThrowsAsync(new System.Exception("Test exception"));

            var result = await translationController.Put(1, translation);

            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("Internal server error", statusCodeResult.Value);
        }
    }
}
