using InstitutionsService.Controllers;
using InstitutionsService.Models;
using InstitutionsService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InstitutionsService.Tests.Controllers
{
    [TestClass]
    public class AddressControllerTests
    {
        private Mock<IAddressService> mockAddressService;
        private Mock<ILogger<AddressController>> mockLogger;
        private AddressController addressController;

        [TestInitialize]
        public void Setup()
        {
            mockAddressService = new Mock<IAddressService>();
            mockLogger = new Mock<ILogger<AddressController>>();
            addressController = new AddressController(mockAddressService.Object, mockLogger.Object);
        }

        [TestMethod]
        public async Task SearchAddress_ReturnsOk_WithAddresses()
        {
            var sampleAddresses = new List<Address>
            {
                new Address { Id = 1, AddressText = "Sample Address 1" },
                new Address { Id = 2, AddressText = "Sample Address 2" }
            };
            mockAddressService.Setup(s => s.SearchAddress(It.IsAny<string>())).ReturnsAsync(sampleAddresses);

            var result = await addressController.SearchAddress("Sample");

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
            var returnedAddresses = okResult.Value as IEnumerable<Address>;
            Assert.IsNotNull(returnedAddresses);
            Assert.AreEqual(2, returnedAddresses.Count());
        }

        [TestMethod]
        public async Task SearchAddress_Returns500_OnException()
        {
            mockAddressService.Setup(s => s.SearchAddress(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));

            var result = await addressController.SearchAddress("Sample");

            var objectResult = result.Result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

        [TestMethod]
        public async Task SearchAddress_ReturnsOk_WithEmptyList_WhenNoMatches()
        {
            mockAddressService.Setup(s => s.SearchAddress(It.IsAny<string>())).ReturnsAsync(new List<Address>());

            var result = await addressController.SearchAddress("NoMatch");

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedAddresses = okResult.Value as IEnumerable<Address>;
            Assert.IsNotNull(returnedAddresses);
            Assert.AreEqual(0, returnedAddresses.Count());
        }
    }
}
