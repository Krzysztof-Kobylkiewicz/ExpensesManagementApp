namespace ExpensesManagementApp.Models.Statistics
{
    public class TransactionsChartSeries
    {
        public TransactionsChartSeries() { }

        public double[] Income { get; set; } = [];
        public double[] Expenses { get; set;} = [];
    }
}
