using Core.Repositories;
using Microsoft.Extensions.Logging;

namespace Core.Tests.Prerequisites
{
    internal class RepositoryTest : Repository<DbContextTest, EntityTest, ModelCoreTest, Guid>
    {
        internal RepositoryTest(DbContextTest dbContext, ILogger<EntityTest> logger) : base(dbContext, logger) { }
    }
}
