using Core.Entities;
using Core.Models;

namespace Core.Filters
{
    internal interface IDbFilterBase<E, DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where DTO : IModelCore<Key> where Key : IEquatable<Key>
    {
        IQueryable<E> Filter(IQueryable<E> query);
    }
}
