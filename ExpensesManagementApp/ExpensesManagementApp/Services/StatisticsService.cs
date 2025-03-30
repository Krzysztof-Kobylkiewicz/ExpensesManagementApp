using ExpensesManagementApp.Client.Services;
using ExpensesManagementApp.Logic.Repositories;
using ExpensesManagementApp.Models.Statistics;

namespace ExpensesManagementApp.Services
{
    public class StatisticsService(StatisticsRepository statisticsRepository, ILogger<StatisticsService> logger) : IStatisticsService
    {
        public async Task<Statistics?> InitializeAsync()
        {
            try
            {
                return await statisticsRepository.InitializeAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Error while accessing statistics repository: {1M}", DateTime.Now, ex.Message);
                throw;
            }
        }
    }
}
