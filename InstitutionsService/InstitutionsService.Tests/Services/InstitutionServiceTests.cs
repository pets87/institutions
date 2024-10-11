using InstitutionsService.Data;
using InstitutionsService.Models;
using InstitutionsService.Services.Impl;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Tests.Services
{
    [TestClass]
    public class InstitutionServiceTests
    {
        private InstitutionService _institutionService;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestInstitutionServiceDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _institutionService = new InstitutionService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetInstitutionsAsync_ShouldReturnInstitutions_WhenCalled()
        {
            var address = new Address() { Id = 3, AddressText = "Tallinn, Main St 1A, 2B" };           
            var institution1 = new Institution { Id = 1, Name = "Institution1", NameTranslationCode = "code1", DeletedDateTime = null, RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            var institution2 = new Institution { Id = 2, Name = "Institution2", NameTranslationCode = "code2", DeletedDateTime = null, RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            _context.Addresses.Add(address);
            _context.Institutions.Add(institution1);
            _context.Institutions.Add(institution2);
            await _context.SaveChangesAsync();

            var result = await _institutionService.GetInstitutionsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Institution1", result.First().Name);
        }

        [TestMethod]
        public async Task GetInstitutionAsync_ShouldReturnInstitution_WhenExists()
        {
            var address = new Address() { Id = 3, AddressText = "Tallinn, Main St 1A, 2B" };
            var institution = new Institution { Id = 1, Name = "Institution1", NameTranslationCode = "code1", DeletedDateTime = null, RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            _context.Addresses.Add(address);
            _context.Institutions.Add(institution);
            await _context.SaveChangesAsync();

            var result = await _institutionService.GetInstitutionAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Institution1", result.Name);
        }

        [TestMethod]
        public async Task GetInstitutionsByIdsAsync_ShouldReturnInstitutions_WhenIdsAreValid()
        {
            var address = new Address() { Id = 3, AddressText = "Tallinn, Main St 1A, 2B" };
            var institution1 = new Institution { Id = 1, Name = "Institution1", NameTranslationCode = "code1", DeletedDateTime = null, RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            var institution2 = new Institution { Id = 2, Name = "Institution2", NameTranslationCode = "code2", DeletedDateTime = null, RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            _context.Addresses.Add(address);
            _context.Institutions.AddRange(institution1, institution2);
            await _context.SaveChangesAsync();

            var result = await _institutionService.GetInstitutionsByIdsAsync(new List<long> { 1, 2 });

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task Delete_ShouldMarkInstitutionAsDeleted_WhenExists()
        {
            var address = new Address() { Id = 3, AddressText = "Tallinn, Main St 1A, 2B" };
            var institution = new Institution { Id = 1, Name = "Institution1", NameTranslationCode = "code1", DeletedDateTime = null, RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            _context.Addresses.Add(address);
            _context.Institutions.Add(institution);
            await _context.SaveChangesAsync();

            var result = await _institutionService.Delete(1);

            Assert.IsTrue(result);
            var deletedInstitution = await _context.Institutions.FindAsync(1L);
            Assert.IsNotNull(deletedInstitution);
            Assert.IsNotNull(deletedInstitution.DeletedDateTime);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnFalse_WhenInstitutionDoesNotExist()
        {
            var result = await _institutionService.Delete(999); // Non-existent ID

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Update_ShouldUpdateInstitution_WhenValidData()
        {
            var institution = new Institution { Id = 1, Name = "Institution1", NameTranslationCode = "code1", RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            _context.Institutions.Add(institution);
            await _context.SaveChangesAsync();

            institution.Name = "UpdatedInstitution";

            var result = await _institutionService.Update(1, institution);

            Assert.AreEqual("UpdatedInstitution", result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Update_ShouldThrowException_WhenIdDoesNotMatch()
        {
            var institution = new Institution { Id = 1, Name = "Institution1", NameTranslationCode = "code1", RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            _context.Institutions.Add(institution);
            await _context.SaveChangesAsync();

            institution.Id = 2;

            await _institutionService.Update(1, institution);
        }

        [TestMethod]
        public async Task Insert_ShouldAddInstitution_WhenValidData()
        {
            var institution = new Institution { Name = "NewInstitution", NameTranslationCode = "code1", RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };

            var result = await _institutionService.Insert(institution);

            Assert.IsNotNull(result);
            Assert.AreEqual("NewInstitution", result.Name);
            Assert.AreEqual(1, _context.Institutions.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Insert_ShouldThrowException_WhenIdIsGreaterThanZero()
        {
            var institution = new Institution { Id = 1, Name = "NewInstitution", NameTranslationCode = "code1", RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };

            await _institutionService.Insert(institution);
        }
    }
}
