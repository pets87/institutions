using InstitutionsService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace InstitutionsService.Tests.Models
{
    [TestClass]
    public class AddressTests
    {        

        [TestMethod]
        public void Address_ColumnAttribute_ShouldBeSetCorrectly()
        {
            var address = new Address();

            var type = typeof(Address);
            var properties = type.GetProperties();

            Assert.AreEqual("country", properties.First(p => p.Name == "Country").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("county", properties.First(p => p.Name == "County").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("city", properties.First(p => p.Name == "City").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("street", properties.First(p => p.Name == "Street").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("house", properties.First(p => p.Name == "House").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("apartment", properties.First(p => p.Name == "Apartment").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("postal_code", properties.First(p => p.Name == "PostalCode").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("address_text", properties.First(p => p.Name == "AddressText").GetCustomAttribute<ColumnAttribute>()?.Name);
        }

        [TestMethod]
        public void Address_ShouldAllowSettingOptionalProperties()
        {
            var address = new Address
            {
                Id = 1,
                Country = "Estonia",
                County = "Harju",
                City = "Tallinn",
                Street = "Main St",
                House = "1A",
                Apartment = "2B",
                PostalCode = "10123",
                AddressText = "Tallinn, Main St 1A, 2B"
            };

            Assert.AreEqual(1, address.Id);
            Assert.AreEqual("Estonia", address.Country);
            Assert.AreEqual("Harju", address.County);
            Assert.AreEqual("Tallinn", address.City);
            Assert.AreEqual("Main St", address.Street);
            Assert.AreEqual("1A", address.House);
            Assert.AreEqual("2B", address.Apartment);
            Assert.AreEqual("10123", address.PostalCode);
            Assert.AreEqual("Tallinn, Main St 1A, 2B", address.AddressText);
        }
    }
}
