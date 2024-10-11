using InstitutionsService.Models;
using InstitutionsService.RabbitMQ.Dto;

namespace InstitutionsService.Tests.RabbitMQ.Dto
{
    [TestClass]
    public class InstitutionDtoTests
    {
        [TestMethod]
        public void InstitutionDto_ShouldHaveIdProperty()
        {
            var institutionDto = new InstitutionDto();

            institutionDto.Id = 1;

            Assert.AreEqual(1, institutionDto.Id);
        }

        [TestMethod]
        public void InstitutionDto_ShouldHaveNameProperty()
        {
            var institutionDto = new InstitutionDto();

            institutionDto.Name = "Test Institution";

            Assert.AreEqual("Test Institution", institutionDto.Name);
        }

        [TestMethod]
        public void InstitutionDto_ShouldHaveRegCodeProperty()
        {
            var institutionDto = new InstitutionDto();

            institutionDto.RegCode = "123456789";

            Assert.AreEqual("123456789", institutionDto.RegCode);
        }

        [TestMethod]
        public void InstitutionDto_ShouldHaveKMKRProperty()
        {
            var institutionDto = new InstitutionDto();

            institutionDto.KMKR = "987654321";

            Assert.AreEqual("987654321", institutionDto.KMKR);
        }

        [TestMethod]
        public void InstitutionDto_ShouldHaveTypeProperty()
        {
            var institutionDto = new InstitutionDto();
            var classifier = new Classifier { Id = 1, Group = "TypeGroup", Name = "TypeName" };

            institutionDto.Type = classifier;

            Assert.AreEqual(classifier, institutionDto.Type);
        }

        [TestMethod]
        public void InstitutionDto_ShouldHaveValidFromProperty()
        {
            var institutionDto = new InstitutionDto();
            var validFromDate = DateTime.UtcNow;

            institutionDto.ValidFrom = validFromDate;

            Assert.AreEqual(validFromDate, institutionDto.ValidFrom);
        }

        [TestMethod]
        public void InstitutionDto_ShouldHaveValidToProperty()
        {
            var institutionDto = new InstitutionDto();
            var validToDate = DateTime.UtcNow.AddDays(30);

            institutionDto.ValidTo = validToDate;

            Assert.AreEqual(validToDate, institutionDto.ValidTo);
        }

        [TestMethod]
        public void InstitutionDto_ShouldHaveAddressProperty()
        {
            var institutionDto = new InstitutionDto();
            var address = new Address { Id = 1, AddressText = "123 Main St" };

            institutionDto.Address = address;

            Assert.AreEqual(address, institutionDto.Address);
        }

        [TestMethod]
        public void InstitutionDto_ShouldHaveTranslationsProperty()
        {
            var institutionDto = new InstitutionDto();
            var translations = new List<Translation>
            {
                new Translation { Id = 1, Code = "test", Text = "Test Translation" }
            };

            institutionDto.Translations = translations;

            Assert.AreEqual(translations, institutionDto.Translations);
        }
    }
}
