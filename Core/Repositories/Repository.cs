using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public abstract class Repository<C> : IRepository<C> where C : DbContext
    {
        protected C _dbContext;

        protected Repository(C dbContext)
        {
            _dbContext = dbContext;
        }

        public int SaveChanges() => _dbContext.SaveChanges();

        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

        public IQueryable<E> GetQuery<E, Key>() where E : Entity<Key> where Key : IEquatable<Key> => _dbContext.Set<E>();

        public IQueryable<E> GetEntityQuery<E, Key>(Func<DbSet<E>, IQueryable<E>> func) where E : Entity<Key> where Key : IEquatable<Key> => func(_dbContext.Set<E>());

        #region EntityRetrieval

        public E[] GetEntities<E, Key>(Func<DbSet<E>, IQueryable<E>> func) where E : Entity<Key> where Key : IEquatable<Key> => GetEntityQuery<E, Key>(func).ToArray();

        public async Task<E[]> GetEntitiesAsync<E, Key>(Func<DbSet<E>, IQueryable<E>> func) where E : Entity<Key> where Key : IEquatable<Key> => await GetEntityQuery<E, Key>(func).ToArrayAsync();

        public E? GetEntity<E, Key>(Key id) where E : Entity<Key> where Key : IEquatable<Key> => GetEntityQuery<E, Key>(entity => entity.Where(e => e.Id.Equals(id))).SingleOrDefault();

        public async Task<E?> GetEntityAsync<E, Key>(Key id) where E : Entity<Key> where Key : IEquatable<Key> => await GetEntityQuery<E, Key>(entity => entity.Where(e => e.Id.Equals(id))).SingleOrDefaultAsync();

        #endregion
    }
}
 