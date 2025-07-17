namespace ExpensesManagementApp.Models.Statistics
{
    public class TransactionAmountAndDate(double amount, DateTime date)
    {
        public double Amount { get; set; } = amount;
        public DateTime Date { get; set; } = date;
    }
}
