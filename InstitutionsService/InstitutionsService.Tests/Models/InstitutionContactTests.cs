using InstitutionsService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace InstitutionsService.Tests.Models
{
    [TestClass]
    public class InstitutionContactTests
    {
        [TestMethod]
        public void InstitutionContact_ShouldHaveRequiredAttributeOnId()
        {
            var propertyInfo = typeof(InstitutionContact).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void InstitutionContact_ShouldHaveKeyAttributeOnId()
        {
            var propertyInfo = typeof(InstitutionContact).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void InstitutionContact_ColumnAttribute_ShouldBeSetCorrectly()
        {
            var type = typeof(InstitutionContact);
            var properties = type.GetProperties();

            Assert.AreEqual("institution_id", properties.First(p => p.Name == "InstitutionId").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("person_id", properties.First(p => p.Name == "PersonId").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("telephone", properties.First(p => p.Name == "Telephone").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("email", properties.First(p => p.Name == "Email").GetCustomAttribute<ColumnAttribute>()?.Name);
        }

        [TestMethod]
        public void InstitutionContact_ShouldNotInitializeProperties()
        {
            var institutionContact = new InstitutionContact();

            Assert.IsNull(institutionContact.Telephone);
            Assert.IsNull(institutionContact.Email);
            Assert.AreEqual(0, institutionContact.InstitutionId);
            Assert.AreEqual(0, institutionContact.PersonId);
        }

        [TestMethod]
        public void InstitutionContact_ShouldInheritFromBaseEntity()
        {
            var type = typeof(InstitutionContact);

            Assert.IsTrue(type.IsSubclassOf(typeof(BaseEntity)));
        }
    }
}
