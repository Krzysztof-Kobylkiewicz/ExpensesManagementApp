using Core.Entities;
using Core.Filters;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public interface IRepository<C> where C : DbContext
    {
        #region SaveChanges

        int SaveChanges();
        Task<int> SaveChangesAsync();

        #endregion

        #region Queries

        IQueryable<E> GetQuery<E, DTO, Key>() where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;
        IQueryable<E> GetEntityQuery<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;

        #endregion

        #region EntityRetrieval

        E[] GetEntities<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;
        Task<E[]> GetEntitiesAsync<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;
        E? GetEntity<E, DTO, Key>(Key id) where E : Entity<E, DTO, Key> where DTO : IModelCore<DTO, Key> where Key : IEquatable<Key>;
        Task<E?> GetEntityAsync<E, DTO, Key>(Key id) where E : Entity<E, DTO, Key> where DTO : IModelCore<DTO, Key> where Key : IEquatable<Key>;

        #region DTO-Retrieval

        DTO? GetEntityDTO<E, DTO, Key>(Key id) where DTO : class, IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key>;
        Task<DTO?> GetEntityDTOAsync<E, DTO, Key>(Key id) where DTO : class, IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key>;
        DTO?[] GetEntitiesDTO<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key>;
        Task<DTO?[]> GetEntitiesDTOAsync<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key>;

        #endregion

        #region FilteredEntities-Retrieval

        E[] GetFilteredEntiteies<E, DTO, Key, F>(F filter, Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> where F : FilterBase<E, DTO, Key>;
        Task<E[]> GetFilteredEntiteiesAsync<E, DTO, Key, F>(F filter, Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> where F : FilterBase<E, DTO, Key>;
        DTO?[] GetFilteredEntiteiesDTO<E, DTO, Key, F>(F filter, Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> where F : FilterBase<E, DTO, Key>;
        Task<DTO?[]> GetFilteredEntiteiesDTOAsync<E, DTO, Key, F>(F filter, Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> where F : FilterBase<E, DTO, Key>;

        #endregion

        bool IsEntityAdded<E, DTO, Key>(Key id) where E : Entity<E, DTO, Key> where DTO : IModelCore<DTO, Key> where Key : IEquatable<Key>;
        Task<bool> IsEntityAddedAsync<E, DTO, Key>(Key id) where E : Entity<E, DTO, Key> where DTO : IModelCore<DTO, Key> where Key : IEquatable<Key>;

        #endregion

        #region EntityAdding

        E? AddEntity<E, DTO, Key>(E entity) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;
        Task<E> AddEntityAsync<E, DTO, Key>(E entity) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;

        #region DTO-Adding

        DTO AddEntityDTO<E, DTO, Key>(DTO dto) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key>;
        Task<DTO> AddEntityDTOAsync<E, DTO, Key>(DTO dto) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key>;

        #endregion

        #endregion

        #region EntityUpdating

        E UpdateEntity<E, DTO, Key>(E entity) where E : Entity<E, DTO, Key> where DTO : IModelCore<DTO, Key> where Key : IEquatable<Key>;
        Task<E> UpdateEntityAsync<E, DTO, Key>(E entity) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;

        #region DTO-Updating

        DTO UpdateEntityDTO<E, DTO, Key>(DTO dto) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key>;
        Task<DTO> UpdateEntityDTOAsync<E, DTO, Key>(DTO dto) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key>;

        #endregion

        #endregion

        #region EntityDeleting

        bool DeleteEntity<E, DTO, Key>(Key id) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;
        Task<bool> DeleteEntityAsync<E, DTO, Key>(Key id) where DTO : IModelCore<DTO, Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>;

        #endregion
    }
}
