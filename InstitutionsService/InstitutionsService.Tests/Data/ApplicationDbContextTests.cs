using InstitutionsService.Data;
using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Tests.Data
{
    [TestClass]
    public class ApplicationDbContextTests
    {
        private ApplicationDbContext? dbContext;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestApplicationDbContextDb")
                .Options;

            dbContext = new ApplicationDbContext(options);
        }

        [TestCleanup]
        public void TearDown()
        {
            dbContext?.Database.EnsureDeleted();
            dbContext?.Dispose();
        }

        [TestMethod]
        public void CanAddAddress()
        {
            var address = new Address
            {
                Id = 1,
                Country = "Eesti",
                County = "Harjumaa",
                City = "Tallinn",
                Street = "Tammsaare tee",
                House = "56",
                PostalCode = "11316",
                AddressText = "Harjumaa, Tallinna, Tammsaare tee 56, 11316"
            };

            dbContext.Addresses.Add(address);
            dbContext.SaveChanges();

            var retrievedAddress = dbContext.Addresses.FirstOrDefault(a => a.Id == address.Id);
            Assert.IsNotNull(retrievedAddress);
            Assert.AreEqual("Eesti", retrievedAddress.Country);
        }

        [TestMethod]
        public void CanAddInstitutionWithAddress()
        {
            var address = new Address
            {
                Id = 1,
                Country = "Eesti",
                County = "Harjumaa",
                City = "Tallinn",
                Street = "Tammsaare tee",
                House = "56",
                PostalCode = "11316",
                AddressText = "Harjumaa, Tallinna, Tammsaare tee 56, 11316"
            };
            dbContext.Addresses.Add(address);
            dbContext.SaveChanges();

            var institution = new Institution
            {
                Id = 1,
                Name = "Srini OÜ",
                RegCode = "14449790",
                KMKR = "EE102059250",
                AddressId = address.Id,
                ValidFrom = DateTime.UtcNow.AddYears(2)
            };

            dbContext.Institutions.Add(institution);
            dbContext.SaveChanges();

            var retrievedInstitution = dbContext.Institutions.Include(i => i.Address).FirstOrDefault(i => i.Id == institution.Id);
            Assert.IsNotNull(retrievedInstitution);
            Assert.AreEqual("Srini OÜ", retrievedInstitution.Name);
            Assert.IsNotNull(retrievedInstitution.Address);
            Assert.AreEqual("Tallinn", retrievedInstitution.Address.City);
        }

        [TestMethod]
        public void CanAddInstitutionContact()
        {
            var address = new Address
            {
                Id = 1,
                Country = "Eesti",
                County = "Harjumaa",
                City = "Tallinn",
                Street = "Tammsaare tee",
                House = "56",
                PostalCode = "11316",
                AddressText = "Harjumaa, Tallinna, Tammsaare tee 56, 11316"
            };
            dbContext.Addresses.Add(address);
            dbContext.SaveChanges();

            var institution = new Institution
            {
                Id = 1,
                Name = "Srini OÜ",
                RegCode = "14449790",
                KMKR = "EE102059250",
                AddressId = address.Id,
                ValidFrom = DateTime.UtcNow.AddYears(2)
            };
            dbContext.Institutions.Add(institution);
            dbContext.SaveChanges();

            var person = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Anderson",
                PersonCode = "38605043778",
                BirthDate = new DateTime(1986, 05, 04, 12, 23, 21, DateTimeKind.Utc)
            };
            dbContext.Persons.Add(person);
            dbContext.SaveChanges();

            var institutionContact = new InstitutionContact
            {
                Id = 1,
                InstitutionId = institution.Id,
                PersonId = person.Id,
                Telephone = "+372555 55 555",
                Email = "info@institution1.com"
            };

            dbContext.InstitutionContacts.Add(institutionContact);
            dbContext.SaveChanges();

            var retrievedContact = dbContext.InstitutionContacts.FirstOrDefault(ic => ic.Id == institutionContact.Id);
            Assert.IsNotNull(retrievedContact);
            Assert.AreEqual("+372555 55 555", retrievedContact.Telephone);
            Assert.IsNotNull(retrievedContact.PersonId);
            Assert.AreEqual(1, retrievedContact.PersonId);
        }

        [TestMethod]
        public void CanAddInstitutionReplication()
        {
            var address = new Address
            {
                Id = 1,
                Country = "Eesti",
                County = "Harjumaa",
                City = "Tallinn",
                Street = "Tammsaare tee",
                House = "56",
                PostalCode = "11316",
                AddressText = "Harjumaa, Tallinna, Tammsaare tee 56, 11316"
            };
            dbContext.Addresses.Add(address);
            dbContext.SaveChanges();

            var institution = new Institution
            {
                Id = 1,
                Name = "Srini OÜ",
                RegCode = "14449790",
                KMKR = "EE102059250",
                AddressId = address.Id,
                ValidFrom = DateTime.UtcNow.AddYears(2)
            };
            dbContext.Institutions.Add(institution);
            dbContext.SaveChanges();

            var institutionReplication = new InstitutionReplication
            {
                Id = 1,
                InstitutionId = institution.Id,
                EnvironmentClassifierId = 1,
                SystemClassifierId = 1,
                PublishedDateTime = DateTime.UtcNow
            };

            dbContext.InstitutionReplications.Add(institutionReplication);
            dbContext.SaveChanges();

            var retrievedReplication = dbContext.InstitutionReplications.FirstOrDefault(ir => ir.Id == institutionReplication.Id);
            Assert.IsNotNull(retrievedReplication);
            Assert.AreEqual(institution.Id, retrievedReplication.InstitutionId);
        }
    }
}
