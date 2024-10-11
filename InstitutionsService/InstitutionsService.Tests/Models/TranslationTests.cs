using InstitutionsService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InstitutionsService.Tests.Models
{
    [TestClass]
    public class TranslationTests
    {
        [TestMethod]
        public void Translation_ShouldHaveRequiredAttributeOnId()
        {
            var propertyInfo = typeof(Translation).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void Translation_ShouldHaveKeyAttributeOnId()
        {
            var propertyInfo = typeof(Translation).GetProperty("Id");

            var attributes = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);

            Assert.IsTrue(attributes.Any());
        }

        [TestMethod]
        public void Translation_ColumnAttribute_ShouldBeSetCorrectly()
        {
            var type = typeof(Translation);
            var properties = type.GetProperties();

            Assert.AreEqual("type", properties.First(p => p.Name == "Type").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("code", properties.First(p => p.Name == "Code").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("language", properties.First(p => p.Name == "Language").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("text", properties.First(p => p.Name == "Text").GetCustomAttribute<ColumnAttribute>()?.Name);
        }

        [TestMethod]
        public void Translation_ShouldNotInitializeProperties()
        {
            var translation = new Translation();

            Assert.IsNull(translation.Type);
            Assert.IsNull(translation.Code);
            Assert.IsNull(translation.Language);
            Assert.IsNull(translation.Text);
        }

        [TestMethod]
        public void Translation_ShouldInheritFromBaseEntity()
        {
            var type = typeof(Translation);

            Assert.IsTrue(type.IsSubclassOf(typeof(BaseEntity)));
        }
    }
}
