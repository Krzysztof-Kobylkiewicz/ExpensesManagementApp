namespace ExpensesManagementApp.Models.Transaction
{
    public class TransactionFiltr : ITransactionFiltr
    {
        public TransactionFiltr() { }

        public DateTime DateFrom { get; set; } = DateTime.Now.Date.AddMonths(-3);
        public DateTime DateTo { get; set; } = DateTime.Now.Date;
        public Statistics.AggregationInterval AggregationInterval { get; set; } = Statistics.AggregationInterval.Yearly;
    }
}
