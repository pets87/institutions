using InstitutionsService.Data;
using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace InstitutionsService.Tests.Data
{
    [TestClass]
    public class DataSeedTests
    {
        private ApplicationDbContext? mockContext;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDataSeedDb")
                .Options;

            mockContext = new ApplicationDbContext(options);
        }

        [TestCleanup]
        public void TearDown()
        {
            mockContext?.Database.EnsureDeleted();
            mockContext?.Dispose();
        }

        [TestMethod]
        public void SeedAddresses_SeedsDataWhenTablesAreEmpty()
        {
            DataSeed.Run(mockContext);

            var addresses = mockContext.Addresses.ToList();
            Assert.AreEqual(20, addresses.Count);
            Assert.IsTrue(addresses.Any(p => p.AddressText.Contains("Harjumaa")));
        }

        [TestMethod]
        public void SeedPersons_SeedsDataWhenTablesAreEmpty()
        {
            DataSeed.Run(mockContext);

            var persons = mockContext.Persons.ToList();
            Assert.AreEqual(3, persons.Count);
            Assert.IsTrue(persons.Any(p => p.FirstName == "John" && p.LastName == "Anderson"));
        }

        [TestMethod]
        public void SeedClassifiers_SeedsDataWhenTablesAreEmpty()
        {
            DataSeed.Run(mockContext);

            var classifiers = mockContext.Classifiers.ToList();
            Assert.AreEqual(11, classifiers.Count);
            Assert.IsTrue(classifiers.Any(c => c.Name == "Osaühing"));
        }

        [TestMethod]
        public void SeedInstitutions_SeedsDataWhenTablesAreEmpty()
        {
            DataSeed.Run(mockContext);

            var institutions = mockContext.Institutions.ToList();
            Assert.AreEqual(5, institutions.Count);
            Assert.IsTrue(institutions.Any(i => i.Name == "Srini OÜ"));
        }

        [TestMethod]
        public void SeedInstitutionContacts_SeedsDataWhenTablesAreEmpty()
        {
            DataSeed.Run(mockContext);

            var institutionContacts = mockContext.InstitutionContacts.ToList();
            Assert.AreEqual(5, institutionContacts.Count);
            Assert.IsTrue(institutionContacts.Any(ic => ic.Telephone == "+372555 55 555"));
        }

        [TestMethod]
        public void SeedTranslations_SeedsDataWhenTablesAreEmpty()
        {
            DataSeed.Run(mockContext);

            var translations = mockContext.Translations.ToList();
            Assert.AreEqual(126, translations.Count);
            Assert.IsTrue(translations.Any(t => t.Text == "Institutions"));
        }

        [TestMethod]
        public void SeedInstitutionReplications_SeedsDataWhenTablesAreEmpty()
        {
            DataSeed.Run(mockContext);

            var institutionReplications = mockContext.InstitutionReplications.ToList();
            Assert.AreEqual(2, institutionReplications.Count);
            Assert.IsTrue(institutionReplications.Any(ir => ir.InstitutionId == 1));
        }

        [TestMethod]
        public void SeedData_DoesNotSeedWhenTablesAreNotEmpty()
        {
            DataSeed.Run(mockContext);
            var initialAddressCount = mockContext.Addresses.Count();

            DataSeed.Run(mockContext);

            var addresses = mockContext.Addresses.ToList();
            Assert.AreEqual(initialAddressCount, addresses.Count);
        }

    }
}
