using Core.Tests.Prerequisites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogus;
using System.Linq.Expressions;

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

        private ModelCoreTest CreateNewInstanceOfDto() => new ModelCoreTest { Id = Guid.NewGuid(), TestProperty = faker.Name.FirstName() };

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

        private List<ModelCoreTest> InitializeListOfModelCoreTestDtos(int n = 10, bool addToDb = true)
        {
            List<ModelCoreTest> dtos = [];

            for (int i = 0; i < n; i++)
            {
                ModelCoreTest dto = CreateNewInstanceOfDto();
                if (addToDb)
                {
                    _repo?.AddEntityDTO<EntityTest, ModelCoreTest, Guid>(dto);
                }
                dtos.Add(dto);
            }

            return dtos;
        }

        private List<EntityTest> InitializeListOfEntitiesTest(int n = 10, bool addToDb = true) => InitializeListOfModelCoreTestDtos(n, addToDb).Select(dto => EntityTest.ConvertDTOToEntity(dto)).ToList();

        [TestMethod]
        public void GetFilteredEntities_ShouldReturnFilteredEntities()
        {
            int n = 10;
            List<ModelCoreTest> dtos = InitializeListOfModelCoreTestDtos(n);

            IEnumerable<EntityTest>? retrievedEntities = _repo?.GetEntities<EntityTest, ModelCoreTest, Guid>(e => e.Where(e => e.Id.Equals(dtos.First().Id) || e.Id.Equals(dtos.Last().Id)));

            Assert.IsNotNull(retrievedEntities);
            Assert.AreEqual(2, retrievedEntities.Count());
            Assert.IsNotNull(retrievedEntities.Where(re => re.Id.Equals(dtos.First().Id) && re.TestProperty?.Equals(dtos.First().TestProperty) == true).SingleOrDefault());
            Assert.IsNotNull(retrievedEntities.Where(re => re.Id.Equals(dtos.Last().Id) && re.TestProperty?.Equals(dtos.Last().TestProperty) == true).SingleOrDefault());
        }

        [TestMethod]
        public void GetFilteredEntitiesDto_ShouldReturnFilteredEntitiesDto()
        {
            int n = 10;
            List<ModelCoreTest> dtos = InitializeListOfModelCoreTestDtos(n);

            IEnumerable<ModelCoreTest?>? retrievedEntities = _repo?.GetEntitiesDTO<EntityTest, ModelCoreTest, Guid>(e => e.Where(e => e.Id.Equals(dtos.First().Id) || e.Id.Equals(dtos.Last().Id)));

            Assert.IsNotNull(retrievedEntities);
            Assert.AreEqual(2, retrievedEntities.Count());
            Assert.IsNotNull(retrievedEntities.Where(re => re?.Equals(dtos.First()) == true).SingleOrDefault());
            Assert.IsNotNull(retrievedEntities.Where(re => re?.Equals(dtos.Last()) == true).SingleOrDefault());
        }

        #endregion

        [TestMethod]
        public void IsEntityAdded_ShouldReturnTrueOnlyIfEntityIsAddedToDatabase()
        {
            ModelCoreTest dto1 = InitializeListOfModelCoreTestDtos(1).First();
            ModelCoreTest dto2 = InitializeListOfModelCoreTestDtos(1, false).First();

            Assert.IsTrue(_repo?.IsEntityAdded<EntityTest, ModelCoreTest, Guid>(dto1.Id));
            Assert.IsFalse(_repo?.IsEntityAdded<EntityTest, ModelCoreTest, Guid>(dto2.Id));
        }

        #region AddingTests

        [TestMethod]
        public void AddEntity_ShouldAddEntityOnlyIfItsNotPresentInDatabase()
        {
            EntityTest entity1 = InitializeListOfEntitiesTest(1).First();

            Assert.IsTrue(_repo?.IsEntityAdded<EntityTest, ModelCoreTest, Guid>(entity1.Id));
            Assert.Throws<InvalidOperationException>(() =>
            {
                try
                {
                    _repo?.AddEntity<EntityTest, ModelCoreTest, Guid>(entity1);
                }
                catch (AggregateException aggex)
                {
                    throw aggex.InnerException ?? new Exception();
                }
            });
        }

        [TestMethod]
        public void AddEntityDto_ShouldAddEntityDtoOnlyIfItsNotPresentInDatabase()
        {
            ModelCoreTest dto1 = InitializeListOfModelCoreTestDtos(1).First();

            Assert.IsTrue(_repo?.IsEntityAdded<EntityTest, ModelCoreTest, Guid>(dto1.Id));
            Assert.Throws<InvalidOperationException>(() =>
            {
                try
                {
                    _repo?.AddEntityDTO<EntityTest, ModelCoreTest, Guid>(dto1);
                }
                catch (AggregateException aggex)
                {
                    throw aggex.InnerException ?? new Exception();
                }
            });
        }

        #endregion

        #region UpdatingTests

        [TestMethod]
        public void UpdateEntity_ShouldUpdateEntityOnlyIfItIsPresentInDatabase()
        {
            EntityTest entity1 = InitializeListOfEntitiesTest(1).First();

            entity1.TestProperty = faker.Name.FirstName();

            EntityTest? updatedEntity = _repo?.UpdateEntity<EntityTest, ModelCoreTest, Guid>(entity1);

            Assert.IsTrue(entity1.Id.Equals(updatedEntity?.Id) && entity1.TestProperty.Equals(updatedEntity.TestProperty));

            EntityTest entity2 = InitializeListOfEntitiesTest(1, false).First();

            Assert.Throws<InvalidOperationException>(() =>
            {
                try
                {
                    _repo?.UpdateEntity<EntityTest, ModelCoreTest, Guid>(entity2);
                }
                catch (AggregateException aggex)
                {
                    throw aggex.InnerException ?? new Exception();
                }
            });
        }

        [TestMethod]
        public void UpdateEntityDto_ShouldUpdateEntityDtoOnlyIfItIsPresentInDatabase()
        {
            ModelCoreTest dto1 = InitializeListOfModelCoreTestDtos(1).First();

            dto1.TestProperty = faker.Name.FirstName();

            ModelCoreTest? updatedDto1 = _repo?.UpdateEntityDTO<EntityTest, ModelCoreTest, Guid>(dto1);

            Assert.IsTrue(dto1.Id.Equals(updatedDto1?.Id) && dto1.TestProperty.Equals(updatedDto1.TestProperty));

            ModelCoreTest dto2 = InitializeListOfModelCoreTestDtos(1, false).First();

            Assert.Throws<InvalidOperationException>(() =>
            {
                try
                {
                    _repo?.UpdateEntityDTO<EntityTest, ModelCoreTest, Guid>(dto2);
                }
                catch (AggregateException aggex)
                {
                    throw aggex.InnerException ?? new Exception();
                }
            });
        }

        #endregion

        #region DeletingTests

        [TestMethod]
        public void DeleteEntity_ShouldDeleteExactlyOneEntity()
        {
            List<EntityTest> entities = InitializeListOfEntitiesTest(2);

            int? n1 = _dbContext?.modelCoreTest.Count();

            _repo?.DeleteEntity<EntityTest, ModelCoreTest, Guid>(entities.First().Id);

            int? n2 = _dbContext?.modelCoreTest.Count();

            Assert.IsTrue(n2 + 1 == n1);
        }

        [TestMethod]
        public void DeleteEntity_ShouldThrowExceptionWhenThereIsNoGivenEntityInDatabase()
        {
            EntityTest entity = InitializeListOfEntitiesTest(1, false).First();

            Assert.Throws<InvalidOperationException>(() =>
            {
                try
                {
                    _repo?.DeleteEntity<EntityTest, ModelCoreTest, Guid>(entity.Id);
                }
                catch (AggregateException aggex)
                {
                    throw aggex.InnerException ?? new Exception();
                }
            });
        }

        #endregion
    }
}
