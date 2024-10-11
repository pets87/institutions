using InstitutionsService.RabbitMQ;
using InstitutionsService.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;

namespace InstitutionsService.Tests.RabbitMQ
{
    [TestClass]
    public class RabbitMqClientTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<ILogger<RabbitMqClient>> _mockLogger;
        private RabbitMqClient _rabbitMqClient;

        [TestInitialize]
        public void Setup()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<RabbitMqClient>>();
        }

        [TestMethod]
        public void Constructor_ShouldThrowException_WhenConnectionStringIsEmpty()
        {
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_CONNECTIONSTRING]).Returns(string.Empty);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_HOST]).Returns(string.Empty);

            Assert.ThrowsException<InvalidOperationException>(() => new RabbitMqClient(_mockConfiguration.Object, _mockLogger.Object));
        }

        [TestMethod]
        public void Constructor_ShouldSetRabbitMQConnectionString_WhenValidConfigurationProvided()
        {
            // Arrange
            var host = "localhost";
            var port = "5672";
            var user = "guest";
            var password = "guest";
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_HOST]).Returns(host);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_PORT]).Returns(port);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_USER]).Returns(user);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_PASSWORD]).Returns(password);

            _rabbitMqClient = new RabbitMqClient(_mockConfiguration.Object, _mockLogger.Object);

            Assert.IsNotNull(_rabbitMqClient);
        }

        [TestMethod]
        public void CreateQueues_ShouldReturnTrue_WhenQueueNamesAreEmpty()
        {
            var host = "localhost";
            var port = "5672";
            var user = "guest";
            var password = "guest";
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_HOST]).Returns(host);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_PORT]).Returns(port);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_USER]).Returns(user);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_PASSWORD]).Returns(password);

            _rabbitMqClient = new RabbitMqClient(_mockConfiguration.Object, _mockLogger.Object);

            var result = _rabbitMqClient.CreateQueues(new List<string>());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateQueues_ShouldReturnFalse_WhenQueueCreationFails()
        {
            var queueNames = new List<string> { "TestQueue" };
            var host = "localhost";
            var port = "5672";
            var user = "guest";
            var password = "guest";
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_HOST]).Returns(host);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_PORT]).Returns(port);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_USER]).Returns(user);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_PASSWORD]).Returns(password);
            _rabbitMqClient = new RabbitMqClient(_mockConfiguration.Object, _mockLogger.Object);

            var connectionMock = new Mock<IConnection>();
            var channelMock = new Mock<IModel>();

            connectionMock.Setup(c => c.CreateModel()).Throws(new Exception("Connection failed"));

            var result = _rabbitMqClient.CreateQueues(queueNames);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Publish_ShouldReturnFalse_WhenPublishingFails()
        {
            var system = "TestSystem";
            var environment = "TestEnvironment";
            var data = "Test Data";

            var host = "localhost";
            var port = "5672";
            var user = "guest";
            var password = "guest";
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_HOST]).Returns(host);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_PORT]).Returns(port);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_USER]).Returns(user);
            _mockConfiguration.Setup(c => c[ConfigurationVariables.RABBIT_PASSWORD]).Returns(password);

            _rabbitMqClient = new RabbitMqClient(_mockConfiguration.Object, _mockLogger.Object);

            var connectionMock = new Mock<IConnection>();
            var channelMock = new Mock<IModel>();

            connectionMock.Setup(c => c.CreateModel()).Throws(new Exception("Connection failed"));

            var result = _rabbitMqClient.Publish(system, environment, data);

            Assert.IsFalse(result);
        }

       
    }
}
