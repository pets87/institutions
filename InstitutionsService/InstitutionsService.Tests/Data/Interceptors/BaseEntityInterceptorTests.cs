using InstitutionsService.Data;
using InstitutionsService.Data.Interceptors;
using InstitutionsService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace InstitutionsService.Tests.Data.Interceptors
{
    [TestClass]
    public class BaseEntityInterceptorTests
    {
        private ApplicationDbContext dbContext;
        private BaseEntityInterceptor interceptor;
        private Mock<IHttpContextAccessor> httpContextAccessorMock;
        private Mock<HttpContext> mockHttpContext;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestBaseEntityInterceptorDb")
                .Options;

            dbContext = new ApplicationDbContext(options);
            httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            mockHttpContext = new Mock<HttpContext>();
            interceptor = new BaseEntityInterceptor(httpContextAccessorMock.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [TestMethod]
        public async Task SavingChangesAsync_UpdatesBaseEntityFields_WhenModified()
        {
            var person = new Person { Id = 5, UpdatedBy = null, FirstName = "John", LastName = "Doe", PersonCode = "11111111111" };

            dbContext.Persons.Add(person);
            dbContext.SaveChanges();

            person.FirstName = "Updated Name";
            var entry = dbContext.Entry(person);
            entry.State = EntityState.Modified;
            var entries = dbContext.ChangeTracker.Entries<BaseEntity>().ToList();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            mockHttpContext.Setup(x => x.Request.Headers).Returns(new HeaderDictionary { { "UserPersonCode", "TestUser" } });

            interceptor.UpdateBaseEntityFields(entries, "TestUser");

            Assert.AreEqual("TestUser", entry.Property(x => x.UpdatedBy).CurrentValue);
            Assert.IsTrue((DateTime)entry.Property(x => x.UpdatedDateTime).CurrentValue > DateTime.MinValue);
        }

        [TestMethod]
        public async Task UpdateBaseEntityFields_SetsDeletedBy_WhenDeleted()
        {
            var person = new Person { Id = 7, UpdatedBy = null, DeletedBy = null, FirstName = "Mark", LastName = "Twain", PersonCode = "33333333333" };

            dbContext.Persons.Add(person);
            dbContext.SaveChanges();

            var entry = dbContext.Entry(person);
            entry.State = EntityState.Modified;
            entry.Property(x => x.DeletedDateTime).CurrentValue = DateTime.UtcNow;
            var entries = dbContext.ChangeTracker.Entries<BaseEntity>().ToList();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            interceptor.UpdateBaseEntityFields(entries, "TestUser");

            Assert.AreEqual("TestUser", entry.Property(x => x.DeletedBy).CurrentValue);
        }

        [TestMethod]
        public async Task UpdateBaseEntityFields_DoesNotSetDeletedBy_WhenNotDeleted()
        {
            var person = new Person { Id = 8, UpdatedBy = null, DeletedBy = null, FirstName = "Alice", LastName = "Wonderland", PersonCode = "44444444444" };

            dbContext.Persons.Add(person);
            dbContext.SaveChanges();

            var entry = dbContext.Entry(person);
            entry.State = EntityState.Modified;
            entry.Property(x => x.DeletedDateTime).CurrentValue = null; // Not deleted
            var entries = dbContext.ChangeTracker.Entries<BaseEntity>().ToList();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
            interceptor.UpdateBaseEntityFields(entries, "TestUser");

            Assert.IsNull(entry.Property(x => x.DeletedBy).CurrentValue);
        }

        [TestMethod]
        public async Task SavingChangesAsync_UpdatesUpdatedDateTime_WhenUserIsNull()
        {
            var person = new Person { Id = 9, UpdatedBy = null, FirstName = "Charlie", LastName = "Brown", PersonCode = "55555555555" };

            dbContext.Persons.Add(person);
            dbContext.SaveChanges();

            person.FirstName = "Updated Charlie";
            var entry = dbContext.Entry(person);
            entry.State = EntityState.Modified;
            var entries = dbContext.ChangeTracker.Entries<BaseEntity>().ToList();

            interceptor.UpdateBaseEntityFields(entries, null);

            Assert.IsTrue((DateTime)entry.Property(x => x.UpdatedDateTime).CurrentValue > DateTime.MinValue);
            Assert.IsNull(entry.Property(x => x.UpdatedBy).CurrentValue);
        }
    }
}
