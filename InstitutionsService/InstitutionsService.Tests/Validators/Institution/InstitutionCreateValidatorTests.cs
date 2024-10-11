using InstitutionsService.Validators.Institution;

namespace InstitutionsService.Tests.Validators.Institution
{
    [TestClass]
    public class InstitutionCreateValidatorTests
    {
        private InstitutionCreateValidator validator;

        [TestInitialize]
        public void Setup()
        {
            validator = new InstitutionCreateValidator();
        }

        [TestMethod]
        public void IsValid_InstitutionIsNull_ReturnsFalse()
        {
            InstitutionsService.Models.Institution institution = null;

            var result = validator.IsValid(institution);

            Assert.IsFalse(result);
            Assert.AreEqual("Institution object cannot be null.", validator.ErrorMessage);
        }

        

        [TestMethod]
        public void IsValid_NameIsNull_ReturnsFalse()
        {
            var institution = new InstitutionsService.Models.Institution { Name = null };

            var result = validator.IsValid(institution);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid name. Name is mandatory.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_RegCodeIsNull_ReturnsFalse()
        {
            var institution = new InstitutionsService.Models.Institution { Name = "Institution1", RegCode = null };

            var result = validator.IsValid(institution);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid RegCode. RegCode is mandatory.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_TypeClassifierIdIsZero_ReturnsFalse()
        {
            var institution = new InstitutionsService.Models.Institution { Name = "Institution1", RegCode = "123", TypeClassifierId = 0 };

            var result = validator.IsValid(institution);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid TypeClassifierId. TypeClassifierId is mandatory and must be greater that 0.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_AddressIdIsZero_ReturnsFalse()
        {
            var institution = new InstitutionsService.Models.Institution { Name = "Institution1", RegCode = "123", TypeClassifierId = 1, AddressId = 0 };

            var result = validator.IsValid(institution);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid AddressId. AddressId is mandatory and must be greater that 0.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_ValidFromIsNull_ReturnsFalse()
        {
            var institution = new InstitutionsService.Models.Institution { Name = "Institution1", RegCode = "123", TypeClassifierId = 1, AddressId = 1 };

            var result = validator.IsValid(institution);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid ValidFrom. ValidFrom is mandatory.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_ValidFromIsMinValue_ReturnsFalse()
        {
            var institution = new InstitutionsService.Models.Institution { Name = "Institution1", RegCode = "123", TypeClassifierId = 1, AddressId = 1, ValidFrom = DateTime.MinValue };

            var result = validator.IsValid(institution);

            Assert.IsFalse(result);
            Assert.AreEqual("Invalid ValidFrom. ValidFrom is mandatory.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_ValidFromLaterThanValidTo_ReturnsFalse()
        {
            var institution = new InstitutionsService.Models.Institution
            {
                Name = "Institution1",
                RegCode = "123",
                TypeClassifierId = 1,
                AddressId = 1,
                ValidFrom = DateTime.Now.AddDays(1),
                ValidTo = DateTime.Now
            };

            var result = validator.IsValid(institution);

            Assert.IsFalse(result);
            Assert.AreEqual("ValidFrom cannot be later than Validto.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_ValidInstitution_ReturnsTrue()
        {
            var institution = new InstitutionsService.Models.Institution
            {
                Name = "Valid Institution",
                RegCode = "123456",
                TypeClassifierId = 1,
                AddressId = 1,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(1)
            };

            var result = validator.IsValid(institution);

            Assert.IsTrue(result);
            Assert.IsNull(validator.ErrorMessage);
        }
    }
}
