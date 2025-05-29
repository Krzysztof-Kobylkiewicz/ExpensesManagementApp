using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public abstract class Repository<C> : IRepository<C> where C : DbContext
    {
        protected readonly C _dbContext;

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

        public bool IsEntityAdded<E, Key>(Key id) where E : Entity<Key> where Key : IEquatable<Key> => GetEntityQuery<E, Key>(entity => entity.Where(e => e.Id.Equals(id))).Any();

        public async Task<bool> IsEntityAddedAsync<E, Key>(Key id) where E : Entity<Key> where Key : IEquatable<Key> => await GetEntityQuery<E, Key>(entity => entity.Where(e => e.Id.Equals(id))).AnyAsync();

        #endregion

        #region EntityAdding

        public E? AddEntity<E, Key>(E entity) where E : Entity<Key> where Key : IEquatable<Key>
        {
            if (IsEntityAdded<E, Key>(entity.Id))
                throw new InvalidOperationException("Entity is already added.");

            Add<E, Key>(entity);
            SaveChanges();
            return entity;
        }

        private void Add<E, Key>(E entity) where E : Entity<Key> where Key : IEquatable<Key> => _dbContext.Set<E>().Add(entity);

        public async Task<E?> AddEntityAsync<E, Key>(E entity) where E : Entity<Key> where Key : IEquatable<Key>
        {
            if (await IsEntityAddedAsync<E, Key>(entity.Id))
                throw new InvalidOperationException("Entity is already added.");

            await AddAsync<E, Key>(entity);
            await SaveChangesAsync();
            return entity;
        }

        private async Task AddAsync<E, Key>(E entity) where E : Entity<Key> where Key : IEquatable<Key> => await _dbContext.Set<E>().AddAsync(entity);

        #endregion

        #region EntityUpdating

        public E? UpdateEntity<E, Key>(E entity) where E : Entity<Key> where Key : IEquatable<Key>
        {
            E retrievedEntity = GetEntity<E, Key>(entity.Id) ?? throw new InvalidOperationException("There is no entity to update.");

            _dbContext.Entry(retrievedEntity).CurrentValues.SetValues(entity);

            SaveChanges();

            return retrievedEntity;
        }

        public async Task<E?> UpdateEntityAsync<E, Key>(E entity) where E : Entity<Key> where Key : IEquatable<Key>
        {
            E retrievedEntity = await GetEntityAsync<E, Key>(entity.Id) ?? throw new InvalidOperationException("There is no entity to update.");

            _dbContext.Entry(retrievedEntity).CurrentValues.SetValues(entity);

            await SaveChangesAsync();

            return retrievedEntity;
        }

        #endregion

        #region EntityDeleting

        public bool DeleteEntity<E, Key>(Key id) where E : Entity<Key> where Key : IEquatable<Key>
        {
            var retrievedEntity = GetEntity<E, Key>(id) ?? throw new InvalidOperationException("There is no entity to delete.");

            _dbContext.Remove(retrievedEntity);

            SaveChanges();

            return true;
        }

        public async Task<bool> DeleteEntityAsync<E, Key>(Key id) where E : Entity<Key> where Key : IEquatable<Key>
        {
            var retrievedEntity = await GetEntityAsync<E, Key>(id) ?? throw new InvalidOperationException("There is no entity to delete.");

            _dbContext.Remove(retrievedEntity);

            await SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
 