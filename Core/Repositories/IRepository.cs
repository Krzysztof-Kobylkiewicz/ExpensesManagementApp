using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public interface IRepository<C> where C : DbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IQueryable<E> GetQuery<E, Key>() where E : Entity<Key> where Key : IEquatable<Key>;
        IQueryable<E> GetEntityQuery<E, Key>(Func<DbSet<E>, IQueryable<E>> func) where E : Entity<Key> where Key : IEquatable<Key>;

        #region EntityRetrieval

        E[] GetEntities<E, Key>(Func<DbSet<E>, IQueryable<E>> func) where E : Entity<Key> where Key : IEquatable<Key>;
        Task<E[]> GetEntitiesAsync<E, Key>(Func<DbSet<E>, IQueryable<E>> func) where E : Entity<Key> where Key : IEquatable<Key>;
        E? GetEntity<E, Key>(Key id) where E : Entity<Key> where Key : IEquatable<Key>;
        Task<E?> GetEntityAsync<E, Key>(Key id) where E : Entity<Key> where Key : IEquatable<Key>;

        #endregion
    }
}
