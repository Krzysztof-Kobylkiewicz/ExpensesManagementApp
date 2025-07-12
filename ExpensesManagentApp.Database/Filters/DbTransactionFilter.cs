using Core.Filters;
using ExpensesManagementApp.Models.Statistics;
using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Database.Filters
{
    public class DbTransactionFilter : FilterBase<DbModels.Transaction, Models.Transaction.Transaction, Guid>, ITransactionFilter
    {
        public DbTransactionFilter() : base() { }
        public DbTransactionFilter(IFilterBase filter) : base(filter) { }
        public DbTransactionFilter(TransactionFilter filter)
        {
            DateTimeFrom = filter.DateTimeFrom;
            DateTimeTo = filter.DateTimeTo;
            IpAddress = filter.IpAddress;
            Login = filter.Login;
            SearchString = filter.SearchString;
            Period = filter.Period;
        }

        public PeriodEnum? Period { get; set; } = PeriodEnum.Quarter;

        public override IQueryable<DbModels.Transaction> Filter(IQueryable<DbModels.Transaction> query)
        {
            FilterDefault(query);

            return query;
        }
    }
}
