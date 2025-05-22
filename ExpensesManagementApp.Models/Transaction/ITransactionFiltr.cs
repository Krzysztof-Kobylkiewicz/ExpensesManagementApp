namespace ExpensesManagementApp.Models.Transaction
{
    public interface ITransactionFiltr
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Statistics.AggregationInterval AggregationInterval { get; set; }
    }
}
