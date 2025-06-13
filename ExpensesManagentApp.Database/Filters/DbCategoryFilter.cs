using Core.Filters;
using ExpensesManagementApp.Database.DbModels;

namespace ExpensesManagementApp.Database.Filters
{
    public class DbCategoryFilter : FilterBase<Category, Models.Category.Category , Guid>
    {
        public DbCategoryFilter() : base() { }
        public DbCategoryFilter(IFilterBase filter) : base(filter) { } 

        public override IQueryable<Category> Filter(IQueryable<Category> query)
        {
            FilterDefault(query);

            return query;
        }
    }
}
