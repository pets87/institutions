using InstitutionsService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace InstitutionsService.Tests.Models
{
    [TestClass]
    public class BaseEntityTests
    {
        [TestMethod]
        public void BaseEntity_ShouldInitializeCreatedDateTime()
        {
            var baseEntity = new BaseEntity();

            Assert.AreNotEqual(default(DateTime), baseEntity.CreatedDateTime);
            Assert.AreEqual(DateTime.UtcNow.Date, baseEntity.CreatedDateTime.Date);
        }

        [TestMethod]
        public void BaseEntity_ShouldInitializeUpdatedDateTime()
        {
            var baseEntity = new BaseEntity();

            Assert.AreNotEqual(default(DateTime?), baseEntity.UpdatedDateTime);
            Assert.AreEqual(DateTime.UtcNow.Date, baseEntity.UpdatedDateTime.Value.Date);
        }

        [TestMethod]
        public void BaseEntity_ColumnAttribute_ShouldBeSetCorrectly()
        {
            var type = typeof(BaseEntity);
            var properties = type.GetProperties();

            Assert.AreEqual("created_datetime", properties.First(p => p.Name == "CreatedDateTime").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("created_by", properties.First(p => p.Name == "CreatedBy").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("updated_datetime", properties.First(p => p.Name == "UpdatedDateTime").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("updated_by", properties.First(p => p.Name == "UpdatedBy").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("deleted_datetime", properties.First(p => p.Name == "DeletedDateTime").GetCustomAttribute<ColumnAttribute>()?.Name);
            Assert.AreEqual("deleted_by", properties.First(p => p.Name == "DeletedBy").GetCustomAttribute<ColumnAttribute>()?.Name);
        }

        [TestMethod]
        public void BaseEntity_ShouldHaveJsonIgnoreAttribute()
        {
            var type = typeof(BaseEntity);
            var properties = type.GetProperties();

            Assert.IsTrue(properties.First(p => p.Name == "CreatedDateTime").GetCustomAttribute<JsonIgnoreAttribute>() != null);
            Assert.IsTrue(properties.First(p => p.Name == "CreatedBy").GetCustomAttribute<JsonIgnoreAttribute>() != null);
            Assert.IsTrue(properties.First(p => p.Name == "UpdatedDateTime").GetCustomAttribute<JsonIgnoreAttribute>() != null);
            Assert.IsTrue(properties.First(p => p.Name == "UpdatedBy").GetCustomAttribute<JsonIgnoreAttribute>() != null);
            Assert.IsTrue(properties.First(p => p.Name == "DeletedDateTime").GetCustomAttribute<JsonIgnoreAttribute>() != null);
            Assert.IsTrue(properties.First(p => p.Name == "DeletedBy").GetCustomAttribute<JsonIgnoreAttribute>() != null);
        }
    }
}
