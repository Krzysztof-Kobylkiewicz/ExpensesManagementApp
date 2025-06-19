using Core.Tests.Prerequisites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogus;

namespace Core.Tests.UnitTests
{
    [TestClass]
    public class RepositoryCoreTests
    {
        private DbContextTest? _dbContext;
        private DbContextOptions<DbContextTest>? _options;
        private RepositoryTest? _repo;
        private ILogger<EntityTest> _logger = LoggerFactory.Create(builder => { }).CreateLogger<EntityTest>();
        private readonly Faker faker = new();

        private ModelCoreTest CreateNewInstanceOfDto() => new ModelCoreTest { Id = Guid.NewGuid(), TestProperty = faker.Name.FirstName()};

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DbContextTest>()
                .UseInMemoryDatabase(databaseName: $"CoreTestsInMemoryDb:{Guid.NewGuid()}")
                .Options;
            _dbContext = new DbContextTest(_options);
            _repo = new RepositoryTest(_dbContext, _logger);
        }

        #region RetrievingTests

        [TestMethod]
        public void GetEntityWhenDatabaseIsEmpty_ShouldReturnNull()
        {
            EntityTest? entity = _repo?.GetEntity<EntityTest, ModelCoreTest, Guid>(Guid.NewGuid());
            Assert.IsNull(entity);
        }

        [TestMethod]
        public void GetEntity_ShoulReturnEntity()
        {
            ModelCoreTest dto = CreateNewInstanceOfDto();

            _repo?.AddEntityDTO<EntityTest, ModelCoreTest, Guid>(dto);

            EntityTest? retrievedEntity = _repo?.GetEntity<EntityTest, ModelCoreTest, Guid>(dto.Id);

            Assert.IsNotNull(retrievedEntity);
            Assert.IsTrue(dto.Id.Equals(retrievedEntity.Id));
            Assert.IsTrue(dto.TestProperty?.Equals(retrievedEntity.TestProperty));
        }

        [TestMethod]
        public void GetEntityDto_ShoulReturnEntityDto()
        {
            ModelCoreTest dto = CreateNewInstanceOfDto();

            _repo?.AddEntityDTO<EntityTest, ModelCoreTest, Guid>(dto);

            ModelCoreTest? retrievedDto = _repo?.GetEntityDTO<EntityTest, ModelCoreTest, Guid>(dto.Id);

            Assert.IsNotNull(retrievedDto);
            Assert.IsTrue(dto.Equals(retrievedDto));
        }

        [TestMethod]
        public void GetAllEntities_ShouldReturnAllEntities()
        {
            int n = 10;

            List<ModelCoreTest> dtos = InitializeListOfModelCoreTestDtos(n);

            IEnumerable<EntityTest>? retrievedEntities = _repo?.GetEntities<EntityTest, ModelCoreTest, Guid>(e => e);

            Assert.IsNotNull(retrievedEntities);
            Assert.AreEqual(n, retrievedEntities.Count());
            Assert.IsTrue(dtos.All(dto => retrievedEntities.Any(entity => entity.Id.Equals(dto.Id) || entity.TestProperty?.Equals(dto.TestProperty) == true)));
        }

        [TestMethod]
        public void GetAllEntitiesDto_ShouldReturnAllEntitiesDto()
        {
            int n = 10;

            List<ModelCoreTest> dtos = InitializeListOfModelCoreTestDtos(n);

            IEnumerable<ModelCoreTest?>? retrievedDtos = _repo?.GetEntitiesDTO<EntityTest, ModelCoreTest, Guid>(e => e);

            Assert.IsNotNull(retrievedDtos);
            Assert.AreEqual(n, retrievedDtos.Count());
            Assert.IsTrue(dtos.All(dto => retrievedDtos.Any(entity => entity?.Id.Equals(dto.Id) == true || entity?.TestProperty?.Equals(dto.TestProperty) == true)));
        }

        private List<ModelCoreTest> InitializeListOfModelCoreTestDtos(int n = 10)
        {
            List<ModelCoreTest> dtos = [];

            for (int i = 0; i < n; i++)
            {
                ModelCoreTest dto = CreateNewInstanceOfDto();
                _repo?.AddEntityDTO<EntityTest, ModelCoreTest, Guid>(dto);
                dtos.Add(dto);
            }

            return dtos;
        }

        #endregion
    }
}
