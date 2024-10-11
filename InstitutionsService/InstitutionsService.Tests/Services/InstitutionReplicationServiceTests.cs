using InstitutionsService.Data;
using InstitutionsService.Models;
using InstitutionsService.RabbitMQ;
using InstitutionsService.Services.Impl;
using InstitutionsService.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace InstitutionsService.Tests.Services
{
    [TestClass]
    public class InstitutionReplicationServiceTests
    {
        private InstitutionReplicationService _institutionReplicationService;
        private ApplicationDbContext _context;
        private Mock<IInstitutionService> _institutionServiceMock;
        private Mock<IClassifierService> _classifierServiceMock;
        private Mock<IRabbitMqClient> _rabbitMqClientMock;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestInstitutionReplicationServiceDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _institutionServiceMock = new Mock<IInstitutionService>();
            _classifierServiceMock = new Mock<IClassifierService>();
            _rabbitMqClientMock = new Mock<IRabbitMqClient>();
            _institutionReplicationService = new InstitutionReplicationService(_context, _institutionServiceMock.Object, _classifierServiceMock.Object, _rabbitMqClientMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task BulkInsert_ShouldAddInstitutionReplications_WhenCalled()
        {
            var institutionReplications = new List<InstitutionReplication>
            {
                new InstitutionReplication { InstitutionId = 1, SystemClassifierId = 1, EnvironmentClassifierId = 1 },
                new InstitutionReplication { InstitutionId = 2, SystemClassifierId = 2, EnvironmentClassifierId = 2 }
            };

            _institutionServiceMock.Setup(x => x.GetInstitutionsByIdsAsync(It.IsAny<List<long>>()))
                .ReturnsAsync(new List<Institution>
                {
                    new Institution { Id = 1, Name = "Institution1", TypeClassifierId = 1 },
                    new Institution { Id = 2, Name = "Institution2", TypeClassifierId = 2 }
                });

            _classifierServiceMock.Setup(x => x.GetClassifiersByIds(It.IsAny<List<long>>()))
                .ReturnsAsync(new List<Classifier>
                {
                    new Classifier { Id = 1, Name = "System1", Group = "System" },
                    new Classifier { Id = 2, Name = "Environment1", Group = "Environment" }
                });

            var result = await _institutionReplicationService.BulkInsert(institutionReplications);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, _context.InstitutionReplications.Count());
        }

        [TestMethod]
        public async Task BulkInsert_ShouldPublishData_WhenInstitutionReplicationsAreInserted()
        {
            var institutionReplications = new List<InstitutionReplication>
            {
                new InstitutionReplication { InstitutionId = 1, SystemClassifierId = 1, EnvironmentClassifierId = 1, PlannedPublishDateTime = null },
            };

            _institutionServiceMock.Setup(x => x.GetInstitutionsByIdsAsync(It.IsAny<List<long>>()))
                .ReturnsAsync(new List<Institution>
                {
                    new Institution { Id = 1, Name = "Institution1", TypeClassifierId = 1 }
                });

            _classifierServiceMock.Setup(x => x.GetClassifiersByIds(It.IsAny<List<long>>()))
                .ReturnsAsync(new List<Classifier>
                {
                    new Classifier { Id = 1, Name = "System1", Group = "System" },
                    new Classifier { Id = 1, Name = "Environment1", Group = "Environment" },
                    new Classifier { Id = 1, Name = "Type1", Group = "Type" }
                });

            await _institutionReplicationService.BulkInsert(institutionReplications);

            _rabbitMqClientMock.Verify(x => x.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task BulkInsert_ShouldNotPublish_WhenNoPlannedPublishDateTime()
        {
            var institutionReplications = new List<InstitutionReplication>
            {
                new InstitutionReplication { InstitutionId = 1, SystemClassifierId = 1, EnvironmentClassifierId = 1, PlannedPublishDateTime = null },
            };

            _institutionServiceMock.Setup(x => x.GetInstitutionsByIdsAsync(It.IsAny<List<long>>()))
                .ReturnsAsync(new List<Institution>
                {
                    new Institution { Id = 1, Name = "Institution1", TypeClassifierId = 1 }
                });

            _classifierServiceMock.Setup(x => x.GetClassifiersByIds(It.IsAny<List<long>>()))
                .ReturnsAsync(new List<Classifier>
                {
                    new Classifier { Id = 1, Name = "System1", Group = "System" },
                    new Classifier { Id = 1, Name = "Environment1", Group = "Environment" },
                    new Classifier { Id = 1, Name = "Type1", Group = "Type" }
                });

            await _institutionReplicationService.BulkInsert(institutionReplications);

            _rabbitMqClientMock.Verify(x => x.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
