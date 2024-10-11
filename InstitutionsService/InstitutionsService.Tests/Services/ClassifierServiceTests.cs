using InstitutionsService.Data;
using InstitutionsService.Models;
using InstitutionsService.Services.Impl;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Tests.Services
{
    [TestClass]
    public class ClassifierServiceTests
    {
        private ClassifierService _classifierService;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestClassifierServiceDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _classifierService = new ClassifierService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAllClassifiers_ShouldReturnEmpty_WhenNoClassifiersExist()
        {
            var result = await _classifierService.GetAllClassifiers();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetAllClassifiers_ShouldReturnAllClassifiers_WhenClassifiersExist()
        {
            _context.Classifiers.Add(new Classifier { Id = 1, Group = "Group1", Name = "Classifier1" });
            _context.Classifiers.Add(new Classifier { Id = 2, Group = "Group2", Name = "Classifier2" });
            await _context.SaveChangesAsync();

            var result = await _classifierService.GetAllClassifiers();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetClassifiersByGroups_ShouldReturnClassifiers_WhenGroupsMatch()
        {
            _context.Classifiers.Add(new Classifier { Id = 1, Group = "Group1", Name = "Classifier1" });
            _context.Classifiers.Add(new Classifier { Id = 2, Group = "Group2", Name = "Classifier2" });
            await _context.SaveChangesAsync();

            var groups = new List<string> { "Group1" };

            var result = await _classifierService.GetClassifiersByGroups(groups);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Classifier1", result.First().Name);
        }

        [TestMethod]
        public async Task GetClassifiersByGroups_ShouldReturnEmpty_WhenNoGroupsMatch()
        {
            _context.Classifiers.Add(new Classifier { Id = 1, Group = "Group1", Name = "Classifier1" });
            await _context.SaveChangesAsync();

            var groups = new List<string> { "Group2" };

            var result = await _classifierService.GetClassifiersByGroups(groups);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetClassifiersByIds_ShouldReturnClassifiers_WhenIdsMatch()
        {
            _context.Classifiers.Add(new Classifier { Id = 1, Group = "Group1", Name = "Classifier1" });
            _context.Classifiers.Add(new Classifier { Id = 2, Group = "Group2", Name = "Classifier2" });
            await _context.SaveChangesAsync();

            var ids = new List<long> { 1 };

            var result = await _classifierService.GetClassifiersByIds(ids);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Classifier1", result.First().Name);
        }

        [TestMethod]
        public async Task GetClassifiersByIds_ShouldReturnEmpty_WhenNoIdsMatch()
        {
            _context.Classifiers.Add(new Classifier { Id = 1, Group = "Group1", Name = "Classifier1" });
            await _context.SaveChangesAsync();

            var ids = new List<long> { 2 };

            var result = await _classifierService.GetClassifiersByIds(ids);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
    }
}
