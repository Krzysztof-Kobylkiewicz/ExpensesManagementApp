namespace ExpensesManagementApp.Models.Statistics
{
    public class StatisticsPackage
    {
        public StatisticsPackage() { }

        public Statistics? Statistics { get; set; }
        public TransactionsChartSeries? TransactionsChartSeries { get; set; }
    }
}
