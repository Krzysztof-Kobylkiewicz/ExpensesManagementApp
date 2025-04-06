using ExpensesManagementApp.Client.Services.StatisticsService;
using ExpensesManagementApp.Logic.Repositories;
using ExpensesManagementApp.Models.CustomExceptions;
using ExpensesManagementApp.Models.HttpResult;
using ExpensesManagementApp.Models.Statistics;

namespace ExpensesManagementApp.Services
{
    public class StatisticsService(StatisticsRepository _statisticsRepository, ILogger<StatisticsService> _logger) : IStatisticsService
    {
        public async Task<HttpResult<Statistics?>> InitializeStatisticsAsync()
        {
            try
            {
                var statistics = await _statisticsRepository.InitializeStatisticsAsync();

                return new HttpResult<Statistics?>(statistics);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] StatisticsService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Statistics?>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while attempt to initialize statistics: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Statistics?>();
            }
        }
    }
}
