using InstitutionsService.Data.Interceptors;
using InstitutionsService.Data;
using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InstitutionsService.Tests.Data.Interceptors
{
    [TestClass]
    public class InstitutionReplicationInterceptorTests
    {
        private ApplicationDbContext dbContext;
        private InstitutionReplicationInterceptor interceptor;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestInstitutionReplicationInterceptorDb")
                .Options;

            dbContext = new ApplicationDbContext(options);
            interceptor = new InstitutionReplicationInterceptor();
        }

        [TestCleanup]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [TestMethod]
        public async Task SavingChangesAsync_SetsPublishedDateTime_WhenInstitutionReplicationAddedWithoutDates()
        {
            var replication = new InstitutionReplication { Id = 1, InstitutionId = 1, PlannedPublishDateTime = null, PublishedDateTime = null };

            dbContext.InstitutionReplications.Add(replication);
            await dbContext.SaveChangesAsync();

            var entry = dbContext.Entry(replication);
            entry.State = EntityState.Added;

            var eventData = new DbContextEventData(null, null, dbContext);
            await interceptor.SavingChangesAsync(eventData, new InterceptionResult<int>(), default);

            Assert.IsNotNull(entry.Property(x => x.PublishedDateTime).CurrentValue);
            Assert.IsTrue(entry.Property(x => x.PublishedDateTime).CurrentValue > DateTime.MinValue);
        }

        [TestMethod]
        public async Task SavingChangesAsync_DoesNotSetPublishedDateTime_WhenInstitutionReplicationAddedWithPublishedDateTime()
        {
            var replication = new InstitutionReplication { Id = 2, InstitutionId = 2, PlannedPublishDateTime = null, PublishedDateTime = DateTime.Now };

            dbContext.InstitutionReplications.Add(replication);
            await dbContext.SaveChangesAsync();

            var entry = dbContext.Entry(replication);
            entry.State = EntityState.Added;

            var eventData = new DbContextEventData(null, null, dbContext);
            await interceptor.SavingChangesAsync(eventData, new InterceptionResult<int>(), default);

            Assert.AreEqual(DateTime.Now.Hour, entry.Property(x => x.PublishedDateTime).CurrentValue.Value.Hour);
            Assert.AreEqual(DateTime.Now.Minute, entry.Property(x => x.PublishedDateTime).CurrentValue.Value.Minute);
        }

        [TestMethod]
        public async Task SavingChangesAsync_DoesNotSetPublishedDateTime_WhenInstitutionReplicationAddedWithPlannedPublishDateTime()
        {
            var replication = new InstitutionReplication { Id = 3, InstitutionId = 3, PlannedPublishDateTime = DateTime.Now.AddHours(1), PublishedDateTime = null };

            dbContext.InstitutionReplications.Add(replication);
            await dbContext.SaveChangesAsync();

            var entry = dbContext.Entry(replication);
            entry.State = EntityState.Added;

            var eventData = new DbContextEventData(null, null, dbContext);
            await interceptor.SavingChangesAsync(eventData, new InterceptionResult<int>(), default);

            Assert.IsNull(entry.Property(x => x.PublishedDateTime).CurrentValue);
        }
    }
}
