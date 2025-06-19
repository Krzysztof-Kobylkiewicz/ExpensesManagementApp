using Core.Filters;
using ExpensesManagementApp.Database.DbModels;

namespace ExpensesManagementApp.Database.Filters
{
    public class DbTransactionFilter : FilterBase<Transaction, Models.Transaction.Transaction, Guid>
    {
        public DbTransactionFilter() : base() { }
        public DbTransactionFilter(IFilterBase filter) : base(filter) { }

        public override IQueryable<Transaction> Filter(IQueryable<Transaction> query)
        {
            FilterDefault(query);

            return query;
        }
    }
}
