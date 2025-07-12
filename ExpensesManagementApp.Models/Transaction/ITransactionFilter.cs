namespace ExpensesManagementApp.Models.Transaction
{
    public interface ITransactionFilter
    {
        Statistics.PeriodEnum? Period { get; set; }
    }
}
