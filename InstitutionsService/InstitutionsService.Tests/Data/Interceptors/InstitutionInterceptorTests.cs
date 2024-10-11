using InstitutionsService.Data;
using InstitutionsService.Data.Interceptors;
using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace InstitutionsService.Tests.Data.Interceptors
{
    [TestClass]
    public class InstitutionInterceptorTests
    {
        private ApplicationDbContext dbContext;
        private InstitutionInterceptor interceptor;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestInstitutionInterceptorDb")
                .Options;

            dbContext = new ApplicationDbContext(options);
            interceptor = new InstitutionInterceptor();
        }

        [TestCleanup]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [TestMethod]
        public async Task SavingChangesAsync_UpdatesNameTranslationCode_WhenInstitutionAdded()
        {
            var institution = new Institution { Id = 1, Name = "Test Institution", RegCode= "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };

            dbContext.Institutions.Add(institution);
            await dbContext.SaveChangesAsync();

            var entry = dbContext.Entry(institution);
            entry.State = EntityState.Modified;
            var eventData = new DbContextEventData(null, null, dbContext);

            await interceptor.SavingChangesAsync(eventData, new InterceptionResult<int>(), default);

            Assert.AreEqual("institution.1.name", entry.Property(x => x.NameTranslationCode).CurrentValue);
        }

        [TestMethod]
        public async Task SavingChangesAsync_UpdatesNameTranslationCode_WhenInstitutionModified()
        {
            var institution = new Institution { Id = 2, Name = "Old Institution", RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };

            dbContext.Institutions.Add(institution);
            await dbContext.SaveChangesAsync();

            institution.Name = "Updated Institution";
            var entry = dbContext.Entry(institution);
            entry.State = EntityState.Modified;

            var eventData = new DbContextEventData(null, null, dbContext);

            await interceptor.SavingChangesAsync(eventData, new InterceptionResult<int>(), default);

            Assert.AreEqual("institution.2.name", entry.Property(x => x.NameTranslationCode).CurrentValue);
        }

        [TestMethod]
        public async Task SavingChangesAsync_MakesAddressUnchanged_WhenInstitutionHasAddress()
        {
            var address = new Address { Id = 3, AddressText = "123 Test St" };
            var institution = new Institution { Id = 3, Name = "Institution With Address", Address = address, RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };

            dbContext.Addresses.Add(address);
            dbContext.Institutions.Add(institution);
            await dbContext.SaveChangesAsync();

            var eventData = new DbContextEventData(null, null, dbContext);

            await interceptor.SavingChangesAsync(eventData, new InterceptionResult<int>(), default);

            Assert.AreEqual(EntityState.Unchanged, dbContext.Entry(address).State);
        }

        [TestMethod]
        public async Task SavingChangesAsync_MakesReplicationsUnchanged_WhenInstitutionHasReplications()
        {
            var institution = new Institution { Id = 4, Name = "Institution With Replications", RegCode = "123", TypeClassifierId = 1, ValidFrom = DateTime.Now, AddressId = 3 };
            institution.Replications = new List<InstitutionReplication>
            {
                new InstitutionReplication { InstitutionId = institution.Id, Id = 1, SystemClassifierId = 2 },
                new InstitutionReplication { InstitutionId = institution.Id, Id = 2, SystemClassifierId = 2 }
            };

            dbContext.Institutions.Add(institution);
            await dbContext.SaveChangesAsync();

            var eventData = new DbContextEventData(null, null, dbContext);

            await interceptor.SavingChangesAsync(eventData, new InterceptionResult<int>(), default);

            Assert.IsTrue(institution.Replications.All(replication => dbContext.Entry(replication).State == EntityState.Unchanged));
        }

      
    }
}
