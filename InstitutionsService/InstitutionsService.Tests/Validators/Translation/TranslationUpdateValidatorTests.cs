using InstitutionsService.Validators.Translation;

namespace InstitutionsService.Tests.Validators.Translation
{
    [TestClass]
    public class TranslationUpdateValidatorTests
    {
        private TranslationUpdateValidator validator;

        [TestInitialize]
        public void Setup()
        {
            validator = new TranslationUpdateValidator();
        }

        [TestMethod]
        public void IsValid_TranslationIsNull_ReturnsFalse()
        {
            InstitutionsService.Models.Translation translation = null;

            var result = validator.IsValid(translation);

            Assert.IsFalse(result);
            Assert.AreEqual("Translation object cannot be null.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_IdIsNotGreaterThanZero_ReturnsFalse()
        {
            var translation = new InstitutionsService.Models.Translation { Id = 0 };

            var result = validator.IsValid(translation);

            Assert.IsFalse(result);
            Assert.AreEqual("Translation object must have Id.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_LanguageIsNullOrEmpty_ReturnsFalse()
        {
            var translation = new InstitutionsService.Models.Translation { Id = 1, Language = null };

            var result = validator.IsValid(translation);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid Language. Language is mandatory.", validator.ErrorMessage);

            translation.Language = string.Empty;

            result = validator.IsValid(translation);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid Language. Language is mandatory.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_TextIsNullOrEmpty_ReturnsFalse()
        {
            var translation = new InstitutionsService.Models.Translation { Id = 1, Language = "en", Text = null };

            var result = validator.IsValid(translation);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid Text. Text is mandatory and cannot be empty string.", validator.ErrorMessage);

            translation.Text = string.Empty;

            result = validator.IsValid(translation);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid Text. Text is mandatory and cannot be empty string.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_ValidTranslation_ReturnsTrue()
        {
            var translation = new InstitutionsService.Models.Translation
            {
                Id = 1,
                Language = "en",
                Text = "Hello"
            };

            var result = validator.IsValid(translation);

            Assert.IsTrue(result);
            Assert.IsNull(validator.ErrorMessage);
        }
    }
}
