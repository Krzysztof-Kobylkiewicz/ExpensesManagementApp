using ExpensesManagementApp.Models.Statistics;

namespace ExpensesManagementApp.Client.Services
{
    public interface IStatisticsService
    {
        Task<Statistics?> InitializeAsync();
    }
}
