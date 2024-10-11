using InstitutionsService.Models;
using InstitutionsService.RabbitMQ;
using InstitutionsService.Services;
using Moq;

namespace InstitutionsService.Tests.RabbitMQ
{
    [TestClass]
    public class RabbitMqInitializerTests
    {
        private Mock<IRabbitMqClient> _mockRabbitMqClient;
        private Mock<IClassifierService> _mockClassifierService;

        [TestInitialize]
        public void Setup()
        {
            _mockRabbitMqClient = new Mock<IRabbitMqClient>();
            _mockClassifierService = new Mock<IClassifierService>();
        }

        [TestMethod]
        public async Task Run_ShouldCreateQueues_WhenClassifiersAreRetrieved()
        {
            var classifiers = new List<Classifier>
            {
                new Classifier { Name = "SystemA", Group = InstitutionsService.Util.Constants.CLASSIFIER_GROUP_REPLICAITON_SYSTEM },
                new Classifier { Name = "SystemB", Group = InstitutionsService.Util.Constants.CLASSIFIER_GROUP_REPLICAITON_SYSTEM },
                new Classifier { Name = "Env1", Group = InstitutionsService.Util.Constants.CLASSIFIER_GROUP_REPLICAITON_ENV },
                new Classifier { Name = "Env2", Group = InstitutionsService.Util.Constants.CLASSIFIER_GROUP_REPLICAITON_ENV }
            };

            _mockClassifierService.Setup(s => s.GetClassifiersByGroups(It.IsAny<List<string>>()))
                .ReturnsAsync(classifiers);

            await RabbitMqInitializer.Run(_mockRabbitMqClient.Object, _mockClassifierService.Object);

            _mockRabbitMqClient.Verify(c => c.CreateQueues(It.Is<List<string>>(q =>
                q.Count == 4 &&
                q.Contains("SystemA_Env1") &&
                q.Contains("SystemA_Env2") &&
                q.Contains("SystemB_Env1") &&
                q.Contains("SystemB_Env2"))), Times.Once);
        }

        [TestMethod]
        public async Task Run_ShouldCallCreateQueues_WhenNoClassifiersAreRetrieved()
        {
            _mockClassifierService.Setup(s => s.GetClassifiersByGroups(It.IsAny<List<string>>()))
                .ReturnsAsync(new List<Classifier>());

            await RabbitMqInitializer.Run(_mockRabbitMqClient.Object, _mockClassifierService.Object);

            _mockRabbitMqClient.Verify(c => c.CreateQueues(It.IsAny<List<string>>()), Times.Once);
        }
    }
}
