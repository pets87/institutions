using InstitutionsService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace InstitutionsService.Tests.Models
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void Person_ShouldHaveRequiredAttributeOnId()
        {
            var propertyInfo = typeof(Person).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void Person_ShouldHaveKeyAttributeOnId()
        {
            var propertyInfo = typeof(Person).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void Person_ColumnAttribute_ShouldBeSetCorrectly()
        {
            var type = typeof(Person);
            var properties = type.GetProperties();

            Assert.AreEqual("first_name", properties.First(p => p.Name == "FirstName").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("last_name", properties.First(p => p.Name == "LastName").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("person_code", properties.First(p => p.Name == "PersonCode").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("birth_date", properties.First(p => p.Name == "BirthDate").GetCustomAttribute<ColumnAttribute>()?.Name);
        }

        [TestMethod]
        public void Person_ShouldNotInitializeProperties()
        {
            var person = new Person();

            Assert.IsNull(person.FirstName);
            Assert.IsNull(person.LastName);
            Assert.IsNull(person.PersonCode);
            Assert.AreEqual(DateTime.MinValue, person.BirthDate);
        }

        [TestMethod]
        public void Person_ShouldInheritFromBaseEntity()
        {
            var type = typeof(Person);

            Assert.IsTrue(type.IsSubclassOf(typeof(BaseEntity)));
        }
    }
}
