using Core.Entities;
using Core.Filters;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public abstract class Repository<Context, Entity, Dto, TKey> : IRepository<Context> where Context : DbContext where Entity : Entity<Entity, Dto, TKey>, IConvertable<Entity, Dto, TKey> where Dto : IModelCore<TKey> where TKey : IEquatable<TKey>
    {
        protected readonly Context _dbContext;
        protected readonly ILogger<Entity> _logger;

        protected Repository(Context dbContext, ILogger<Entity> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region SaveChanges
        public int SaveChanges() => _dbContext.SaveChanges();

        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

        #endregion

        #region Queries

        public IQueryable<E> GetQuery<E, DTO, Key>() where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => _dbContext.Set<E>();

        public IQueryable<E> GetEntityQuery<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => func(_dbContext.Set<E>());

        #endregion

        #region EntityRetrieval

        public E[] GetEntities<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => GetEntitiesAsync<E, DTO, Key>(func).Result;

        public async Task<E[]> GetEntitiesAsync<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => await GetEntityQuery<E, DTO, Key>(func).ToArrayAsync();

        public E? GetEntity<E, DTO, Key>(Key id) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => GetEntityAsync<E, DTO, Key>(id).Result;

        public async Task<E?> GetEntityAsync<E, DTO, Key>(Key id) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => (await GetEntitiesAsync<E, DTO, Key>(entity => entity.Where(e => e.Id.Equals(id)))).SingleOrDefault();

        #region DTO-Retrieval

        public DTO? GetEntityDTO<E, DTO, Key>(Key id) where DTO : class, IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> => GetEntityDTOAsync<E, DTO, Key>(id).Result; //GetEntity<E, DTO, Key>(id)?.ConvertEntityToDTO();

        public async Task<DTO?> GetEntityDTOAsync<E, DTO, Key>(Key id) where DTO : class, IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> => (await GetEntityAsync<E, DTO, Key>(id))?.ConvertEntityToDTO();

        public DTO?[] GetEntitiesDTO<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> => GetEntitiesDTOAsync<E, DTO, Key>(func).Result;

        public async Task<DTO?[]> GetEntitiesDTOAsync<E, DTO, Key>(Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> => (await GetEntitiesAsync<E, DTO, Key>(func))?.Select(e => e.ConvertEntityToDTO()).ToArray() ?? [];

        #endregion

        #region FilteredEntities-Retrieval

        public E[] GetFilteredEntiteies<E, DTO, Key, F>(F filter, Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> where F : FilterBase<E, DTO, Key> => GetFilteredEntiteiesAsync<E, DTO, Key, F>(filter, func).Result;

        public async Task<E[]> GetFilteredEntiteiesAsync<E, DTO, Key, F>(F filter, Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> where F : FilterBase<E, DTO, Key> => await filter.Filter(GetEntityQuery<E, DTO, Key>(func)).ToArrayAsync();

        public DTO?[] GetFilteredEntiteiesDTO<E, DTO, Key, F>(F filter, Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> where F : FilterBase<E, DTO, Key> => GetFilteredEntiteiesDTOAsync<E, DTO, Key, F>(filter, func).Result;

        public async Task<DTO?[]> GetFilteredEntiteiesDTOAsync<E, DTO, Key, F>(F filter, Func<DbSet<E>, IQueryable<E>> func) where DTO : class, IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> where F : FilterBase<E, DTO, Key> => (await GetFilteredEntiteiesAsync<E, DTO, Key, F>(filter, func)).Select(e => e.ConvertEntityToDTO()).ToArray();

        #endregion

        public bool IsEntityAdded<E, DTO, Key>(Key id) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => IsEntityAddedAsync<E, DTO, Key>(id).Result;

        public async Task<bool> IsEntityAddedAsync<E, DTO, Key>(Key id) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => await GetEntityQuery<E, DTO, Key>(entity => entity.Where(e => e.Id.Equals(id))).AnyAsync();

        #endregion

        #region EntityAdding

        public E AddEntity<E, DTO, Key>(E entity) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => AddEntityAsync<E, DTO, Key>(entity).Result;

        public async Task<E> AddEntityAsync<E, DTO, Key>(E entity) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>
        {
            if (await IsEntityAddedAsync<E, DTO, Key>(entity.Id))
                throw new InvalidOperationException("Entity is already added.");

            entity.UpoloadDate = DateTime.Now;

            if (typeof(Key) == typeof(Guid) && Equals(entity.Id, default(Key)))
            {
                entity.Id = (Key)(object)Guid.NewGuid();
            }

            await AddAsync<E, DTO, Key>(entity);
            return await SaveChangesAsync() == 1 ? entity : throw new Exception("Entity was not added to the database.");
        }

        #region DTO-Adding

        public DTO AddEntityDTO<E, DTO, Key>(DTO dto) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> => AddEntityDTOAsync<E, DTO, Key>(dto).Result;

        public async Task<DTO> AddEntityDTOAsync<E, DTO, Key>(DTO dto) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> => (await AddEntityAsync<E, DTO, Key>(E.ConvertDTOToEntity(dto))).ConvertEntityToDTO();

        private async Task AddAsync<E, DTO, Key>(E entity) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => await _dbContext.Set<E>().AddAsync(entity);

        #endregion

        #endregion

        #region EntityUpdating

        public E UpdateEntity<E, DTO, Key>(E entity) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => UpdateEntityAsync<E, DTO, Key>(entity).Result;

        public async Task<E> UpdateEntityAsync<E, DTO, Key>(E entity) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>
        {
            E retrievedEntity = await GetEntityAsync<E, DTO, Key>(entity.Id) ?? throw new InvalidOperationException("There is no entity to update.");

            entity.UpdateDate = DateTime.Now;    
            _dbContext.Entry(retrievedEntity).CurrentValues.SetValues(entity);

            await SaveChangesAsync();

            return retrievedEntity;
        }

        #region DTO-Updating

        public DTO UpdateEntityDTO<E, DTO, Key>(DTO dto) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> => UpdateEntityDTOAsync<E, DTO, Key>(dto).Result;

        public async Task<DTO> UpdateEntityDTOAsync<E, DTO, Key>(DTO dto) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where Key : IEquatable<Key> => (await UpdateEntityAsync<E, DTO, Key>(E.ConvertDTOToEntity(dto))).ConvertEntityToDTO();

        #endregion

        #endregion

        #region EntityDeleting

        public bool DeleteEntity<E, DTO, Key>(Key id) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key> => DeleteEntityAsync<E, DTO, Key>(id).Result;

        public async Task<bool> DeleteEntityAsync<E, DTO, Key>(Key id) where DTO : IModelCore<Key> where E : Entity<E, DTO, Key> where Key : IEquatable<Key>
        {
            var retrievedEntity = await GetEntityAsync<E, DTO, Key>(id) ?? throw new InvalidOperationException("There is no entity to delete.");

            _dbContext.Remove(retrievedEntity);

            await SaveChangesAsync();

            return true;
        }

        #endregion
    }
}