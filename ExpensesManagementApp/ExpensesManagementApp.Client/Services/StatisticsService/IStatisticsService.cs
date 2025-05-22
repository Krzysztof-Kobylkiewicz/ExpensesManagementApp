using ExpensesManagementApp.Models.Statistics;
using ExpensesManagementApp.Models.HttpResult;
using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Client.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<HttpResult<Statistics?>> InitializeStatisticsAsync();
        Task<HttpResult<TransactionsChartSeries>> GetChartSeriesAsync(TransactionFiltr transactionFiltr);
        Task<HttpResult<StatisticsPackage>> GetStatisticsPackageAsync(TransactionFiltr transactionFiltr);
    }
}
