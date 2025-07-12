using ExpensesManagementApp.Models.Statistics;
using Core.Models;
using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Client.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<HttpResult<Statistics?>> InitializeStatisticsAsync();
        Task<HttpResult<TransactionsChartSeries>> GetChartSeriesAsync(TransactionFilter transactionFiltr);
        Task<HttpResult<StatisticsPackage>> GetStatisticsPackageAsync(TransactionFilter transactionFiltr);
    }
}
