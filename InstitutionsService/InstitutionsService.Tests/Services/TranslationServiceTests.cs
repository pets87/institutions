using InstitutionsService.Data;
using InstitutionsService.Models;
using InstitutionsService.Services.Impl;
using Microsoft.EntityFrameworkCore;

namespace InstitutionsService.Tests.Services
{
    [TestClass]
    public class TranslationServiceTests
    {
        private TranslationService _translationService;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestTranslationServiceDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _translationService = new TranslationService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAllTranslations_ShouldReturnAllTranslations_WhenCalled()
        {
            var translation1 = new Translation { Id = 1, Code = "code1", Text = "Hello", Language = "en", Type="any" };
            var translation2 = new Translation { Id = 2, Code = "code2", Text = "World", Language = "en", Type = "any" };
            _context.Translations.AddRange(translation1, translation2);
            await _context.SaveChangesAsync();

            var result = await _translationService.GetAllTranslations();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Hello", result.First().Text);
        }

        [TestMethod]
        public async Task Update_ShouldUpdateTranslation_WhenValidData()
        {
            var translation = new Translation { Id = 1, Code = "code1", Text = "Hello", Language = "en", Type = "any" };
            _context.Translations.Add(translation);
            await _context.SaveChangesAsync();

            translation.Text = "Updated Text";

            var result = await _translationService.Update(1, translation);

            Assert.AreEqual("Updated Text", result.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Update_ShouldThrowException_WhenIdDoesNotMatch()
        {
            var translation = new Translation { Id = 1, Code = "code1", Text = "Hello", Language = "en", Type = "any" };
            _context.Translations.Add(translation);
            await _context.SaveChangesAsync();

            translation.Id = 2;

            await _translationService.Update(1, translation);
        }
    }
}
