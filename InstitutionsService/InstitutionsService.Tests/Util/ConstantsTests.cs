using InstitutionsService.Util;

namespace InstitutionsService.Tests.Util
{
    [TestClass]
    public class ConstantsTests
    {
        [TestMethod]
        public void ClassifierGroupReplicationEnv_ShouldHaveExpectedValue()
        {
            Assert.AreEqual("REPLICAITON_ENV", Constants.CLASSIFIER_GROUP_REPLICAITON_ENV);
        }

        [TestMethod]
        public void ClassifierGroupReplicationSystem_ShouldHaveExpectedValue()
        {
            Assert.AreEqual("REPLICAITON_SYSTEM", Constants.CLASSIFIER_GROUP_REPLICAITON_SYSTEM);
        }
    }

    [TestClass]
    public class ConfigurationVariablesTests
    {
        [TestMethod]
        public void RabbitConnectionstring_ShouldHaveExpectedValue()
        {
            Assert.AreEqual("RabbitConnectionstring", ConfigurationVariables.RABBIT_CONNECTIONSTRING);
        }

        [TestMethod]
        public void RabbitHost_ShouldHaveExpectedValue()
        {
            Assert.AreEqual("RabbitHost", ConfigurationVariables.RABBIT_HOST);
        }

        [TestMethod]
        public void RabbitPort_ShouldHaveExpectedValue()
        {
            Assert.AreEqual("RabbitPort", ConfigurationVariables.RABBIT_PORT);
        }

        [TestMethod]
        public void RabbitUser_ShouldHaveExpectedValue()
        {
            Assert.AreEqual("RabbitUser", ConfigurationVariables.RABBIT_USER);
        }

        [TestMethod]
        public void RabbitPassword_ShouldHaveExpectedValue()
        {
            Assert.AreEqual("RabbitPassword", ConfigurationVariables.RABBIT_PASSWORD);
        }
    }
}
