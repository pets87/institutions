using InstitutionsService.Data;
using InstitutionsService.Models;
using InstitutionsService.Services.Impl;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Tests.Services
{
    [TestClass]
    public class AddressServiceTests
    {
        private AddressService _addressService;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAddressServiceDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _addressService = new AddressService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task SearchAddress_ShouldReturnEmpty_WhenTextIsLessThanThreeCharacters()
        {
            var result = await _addressService.SearchAddress("ab");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task SearchAddress_ShouldReturnAddresses_WhenTextIsThreeOrMoreCharacters()
        {
            _context.Add(new Address { Id = 1, AddressText = "123 Main St" });
            _context.Add(new Address { Id = 2, AddressText = "456 Elm St" });
            _context.Add(new Address { Id = 3, AddressText = "789 Oak St" });
            await _context.SaveChangesAsync();

            var result = await _addressService.SearchAddress("Main");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("123 Main St", result.First().AddressText);
        }

        [TestMethod]
        public async Task SearchAddress_ShouldReturnEmpty_WhenNoMatchesFound()
        {
            _context.Add(new Address { Id = 1, AddressText = "123 Main St" });
            _context.Add(new Address { Id = 2, AddressText = "456 Elm St" });
            await _context.SaveChangesAsync();

            var result = await _addressService.SearchAddress("Nonexistent");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
    }
}
