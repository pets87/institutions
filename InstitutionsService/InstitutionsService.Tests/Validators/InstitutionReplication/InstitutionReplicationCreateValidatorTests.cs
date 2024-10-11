using InstitutionsService.Validators.InstitutionReplication;

namespace InstitutionsService.Tests.Validators.InstitutionReplication
{
    [TestClass]
    public class InstitutionReplicationCreateValidatorTests
    {
        private InstitutionReplicationCreateValidator validator;

        [TestInitialize]
        public void Setup()
        {
            validator = new InstitutionReplicationCreateValidator();
        }

        [TestMethod]
        public void IsValid_InstitutionReplicationsListIsNull_ReturnsFalse()
        {
            List<InstitutionsService.Models.InstitutionReplication> institutionReplications = null;

            var result = validator.IsValid(institutionReplications);

            Assert.IsFalse(result);
            Assert.AreEqual("InstitutionReplication List of objects cannot be null.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_ElementIsNull_ReturnsFalse()
        {
            var institutionReplications = new List<InstitutionsService.Models.InstitutionReplication> { null };

            var result = validator.IsValid(institutionReplications);

            Assert.IsFalse(result);
            Assert.AreEqual("Element at position 0: InstitutionReplication object cannot be null.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_IdGreaterThanZero_ReturnsFalse()
        {
            var institutionReplications = new List<InstitutionsService.Models.InstitutionReplication>
            {
                new InstitutionsService.Models.InstitutionReplication { Id = 1 }
            };

            var result = validator.IsValid(institutionReplications);

            Assert.IsFalse(result);
            Assert.AreEqual("Element at position 0: Invalid Id. Id must null or 0.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_InvalidInstitutionId_ReturnsFalse()
        {
            var institutionReplications = new List<InstitutionsService.Models.InstitutionReplication>
            {
                new InstitutionsService.Models.InstitutionReplication { InstitutionId = 0 }
            };

            var result = validator.IsValid(institutionReplications);

            Assert.IsFalse(result);
            Assert.AreEqual("Element at position 0: Invalid InstitutionId. InstitutionId is mandatory and must be greater than 0.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_InvalidEnvironmentClassifierId_ReturnsFalse()
        {
            var institutionReplications = new List<InstitutionsService.Models.InstitutionReplication>
            {
                new InstitutionsService.Models.InstitutionReplication { InstitutionId = 1, EnvironmentClassifierId = 0 }
            };

            var result = validator.IsValid(institutionReplications);

            Assert.IsFalse(result);
            Assert.AreEqual("Element at position 0: Invalid EnvironmentClassifierId. EnvironmentClassifierId is mandatory and must be greater than 0.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_InvalidSystemClassifierId_ReturnsFalse()
        {
            var institutionReplications = new List<InstitutionsService.Models.InstitutionReplication>
            {
                new InstitutionsService.Models.InstitutionReplication { InstitutionId = 1, EnvironmentClassifierId = 1, SystemClassifierId = 0 }
            };

            var result = validator.IsValid(institutionReplications);

            Assert.IsFalse(result);
            Assert.AreEqual("Element at position 0: Invalid SystemClassifierId. SystemClassifierId is mandatory and must be greater than 0.", validator.ErrorMessage);
        }

        [TestMethod]
        public void IsValid_ValidInstitutionReplications_ReturnsTrue()
        {
            var institutionReplications = new List<InstitutionsService.Models.InstitutionReplication>
            {
                new InstitutionsService.Models.InstitutionReplication
                {
                    InstitutionId = 1,
                    EnvironmentClassifierId = 1,
                    SystemClassifierId = 1
                }
            };

            var result = validator.IsValid(institutionReplications);

            Assert.IsTrue(result);
            Assert.IsNull(validator.ErrorMessage); // No error message for valid institution replications
        }

        [TestMethod]
        public void IsValid_MultipleValidInstitutionReplications_ReturnsTrue()
        {
            var institutionReplications = new List<InstitutionsService.Models.InstitutionReplication>
            {
                new InstitutionsService.Models.InstitutionReplication
                {
                    InstitutionId = 1,
                    EnvironmentClassifierId = 1,
                    SystemClassifierId = 1
                },
                new InstitutionsService.Models.InstitutionReplication
                {
                    InstitutionId = 2,
                    EnvironmentClassifierId = 2,
                    SystemClassifierId = 2
                }
            };

            var result = validator.IsValid(institutionReplications);

            Assert.IsTrue(result);
            Assert.IsNull(validator.ErrorMessage);
        }
    }
}
