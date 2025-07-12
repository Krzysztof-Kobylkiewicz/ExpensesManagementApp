using Core.Filters;
using ExpensesManagementApp.Models.Statistics;

namespace ExpensesManagementApp.Models.Transaction
{
    public class TransactionFilter : IFilterBase, ITransactionFilter
    {
        public DateTime? DateTimeFrom { get; set; }
        public DateTime? DateTimeTo { get; set; }
        public string? Login { get; set; }
        public string? IpAddress { get; set; }
        public string? SearchString { get; set; }
        public PeriodEnum? Period { get; set; } = Statistics.PeriodEnum.Quarter;
        public void ClearFilterBase()
        {
            DateTimeFrom = null;
            DateTimeTo = null;
            Login = null;
            SearchString = null;
        }

        public bool IsRangeEditable() => Period.Equals(PeriodEnum.Other);
    }
}
