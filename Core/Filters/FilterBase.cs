using Core.Entities;
using Core.Models;

namespace Core.Filters
{
    public abstract class FilterBase<E, DTO, Key> : IFilterBase, IDbFilterBase<E, DTO, Key> where E : Entity<E, DTO, Key>, IConvertable<E, DTO, Key> where DTO : IModelCore<DTO, Key> where Key : IEquatable<Key>
    {
        public FilterBase() { }

        public FilterBase(IFilterBase filter)
        {
            DateTimeFrom = filter.DateTimeFrom;
            DateTimeTo = filter.DateTimeTo;
            IpAddress = filter.IpAddress;
            Login = filter.Login;
            SearchString = filter.SearchString;
        }

        public DateTime? DateTimeFrom { get; set; }
        public DateTime? DateTimeTo { get; set; }
        public string? IpAddress { get; set; }
        public string? Login { get; set; }
        public string? SearchString { get; set; }
        public void ClearFilterBase()
        {
            DateTimeFrom = null;
            DateTimeTo = null;
            Login = null;
            SearchString = null;
        }
        public virtual IQueryable<E> Filter(IQueryable<E> query) => FilterDefault(query);

        protected IQueryable<E> FilterDefault(IQueryable<E> query)
        {
            if (DateTimeFrom.HasValue)
            {
                query = query.Where(c => c.UpoloadDate.HasValue && c.UpoloadDate.Value.Date >= DateTimeFrom.Value.Date);
            }

            if (DateTimeTo.HasValue)
            {
                query = query.Where(c => c.UpoloadDate.HasValue && c.UpoloadDate.Value.Date <= DateTimeTo.Value.Date);
            }

            if (!string.IsNullOrEmpty(Login))
            {
                query = query.Where(c => !string.IsNullOrEmpty(c.Login) && c.Login.Contains(Login));
            }

            if (!string.IsNullOrEmpty(IpAddress))
            {
                query = query.Where(c => !string.IsNullOrEmpty(c.IpAddress) && c.IpAddress.Contains(IpAddress));
            }

            return query;
        }
    }
}
