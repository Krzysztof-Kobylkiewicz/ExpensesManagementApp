using MudBlazor;

namespace ExpensesManagementApp.Models.Statistics
{
    public class StatisticsPackage
    {
        public StatisticsPackage() { }

        public Statistics? Statistics { get; set; }

        public TransactionsChartSeries? TransactionsChartSeries { get; set; }

        public DateOnly? LatestTransactionDate { get; set; }

        public DateOnly? EarliestTransactionDate { get; set; }

        public DateRange PackageTransactionsDateRange() => EarliestTransactionDate.HasValue && LatestTransactionDate.HasValue ? new (new DateTime(EarliestTransactionDate.Value, new TimeOnly()), new DateTime(LatestTransactionDate.Value, new TimeOnly())) : new DateRange();
    }
}
