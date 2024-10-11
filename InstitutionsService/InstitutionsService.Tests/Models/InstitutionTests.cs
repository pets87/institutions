using InstitutionsService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace InstitutionsService.Tests.Models
{
    [TestClass]
    public class InstitutionTests
    {
        [TestMethod]
        public void Institution_ShouldHaveRequiredAttributeOnId()
        {
            var propertyInfo = typeof(Institution).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void Institution_ShouldHaveKeyAttributeOnId()
        {
            var propertyInfo = typeof(Institution).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void Institution_ColumnAttribute_ShouldBeSetCorrectly()
        {
            var type = typeof(Institution);
            var properties = type.GetProperties();

            Assert.AreEqual("name", properties.First(p => p.Name == "Name").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("name_translation_code", properties.First(p => p.Name == "NameTranslationCode").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("reg_code", properties.First(p => p.Name == "RegCode").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("kmkr", properties.First(p => p.Name == "KMKR").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("address_id", properties.First(p => p.Name == "AddressId").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("type_classifier_id", properties.First(p => p.Name == "TypeClassifierId").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("valid_from", properties.First(p => p.Name == "ValidFrom").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("valid_to", properties.First(p => p.Name == "ValidTo").GetCustomAttribute<ColumnAttribute>()?.Name);
        }

        [TestMethod]
        public void Institution_ShouldNotInitializeProperties()
        {
            var institution = new Institution();

            Assert.IsNull(institution.Name);
            Assert.IsNull(institution.NameTranslationCode);
            Assert.IsNull(institution.RegCode);
            Assert.IsNull(institution.KMKR);
            Assert.AreEqual(0, institution.AddressId);
            Assert.AreEqual(0, institution.TypeClassifierId);
            Assert.AreEqual(DateTime.MinValue, institution.ValidFrom);
            Assert.IsNull(institution.ValidTo);
            Assert.IsNull(institution.Address);
            Assert.IsNull(institution.Translations);
            Assert.IsNull(institution.Replications);
        }

        [TestMethod]
        public void Institution_ShouldInheritFromBaseEntity()
        {
            var type = typeof(Institution);

            Assert.IsTrue(type.IsSubclassOf(typeof(BaseEntity)));
        }

        [TestMethod]
        public void Institution_ShouldHaveListOfTranslations()
        {
            var institution = new Institution();
            institution.Translations = new List<Translation>
            {
                new Translation(),
                new Translation()
            };

            Assert.AreEqual(2, institution.Translations.Count);
        }

        [TestMethod]
        public void Institution_ShouldHaveListOfReplications()
        {
            var institution = new Institution();
            institution.Replications = new List<InstitutionReplication>
            {
                new InstitutionReplication(),
                new InstitutionReplication()
            };

            Assert.AreEqual(2, institution.Replications.Count);
        }
    }
}
