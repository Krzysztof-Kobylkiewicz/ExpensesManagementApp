using Microsoft.EntityFrameworkCore;

namespace Core.Tests.Prerequisites
{
    internal class DbContextTest(DbContextOptions<DbContextTest> options) : DbContext(options)
    {
        public DbSet<EntityTest> modelCoreTest { get; set; }
    }
}
