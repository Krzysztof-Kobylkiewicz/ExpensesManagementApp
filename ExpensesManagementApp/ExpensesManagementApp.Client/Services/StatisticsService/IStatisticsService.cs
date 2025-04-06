using ExpensesManagementApp.Models.Statistics;
using ExpensesManagementApp.Models.HttpResult;

namespace ExpensesManagementApp.Client.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<HttpResult<Statistics?>> InitializeStatisticsAsync();
    }
}
