using InstitutionsService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace InstitutionsService.Tests.Models
{
    [TestClass]
    public class ClassifierTests
    {
        [TestMethod]
        public void Classifier_ShouldHaveRequiredAttributeOnId()
        {
            var propertyInfo = typeof(Classifier).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void Classifier_ShouldHaveKeyAttributeOnId()
        {
            var propertyInfo = typeof(Classifier).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void Classifier_ColumnAttribute_ShouldBeSetCorrectly()
        {
            var type = typeof(Classifier);
            var properties = type.GetProperties();

            Assert.AreEqual("group", properties.First(p => p.Name == "Group").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("name", properties.First(p => p.Name == "Name").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("name_translation_code", properties.First(p => p.Name == "NameTranslationCode").GetCustomAttribute<ColumnAttribute>()?.Name);
        }

        [TestMethod]
        public void Classifier_ShouldNotInitializeProperties()
        {
            var classifier = new Classifier();

            Assert.IsNull(classifier.Group);
            Assert.IsNull(classifier.Name);
            Assert.IsNull(classifier.NameTranslationCode);
        }

        [TestMethod]
        public void Classifier_ShouldInheritFromBaseEntity()
        {
            var type = typeof(Classifier);

            Assert.IsTrue(type.IsSubclassOf(typeof(BaseEntity)));
        }
    }
}
