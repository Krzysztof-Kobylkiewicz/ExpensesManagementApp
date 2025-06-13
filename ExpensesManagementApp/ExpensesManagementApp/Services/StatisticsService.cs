using Core.Filters;
using ExpensesManagementApp.Client.Services.StatisticsService;
using ExpensesManagementApp.Database.Filters;
using ExpensesManagementApp.Logic.Repositories;
using ExpensesManagementApp.Models.CustomExceptions;
using ExpensesManagementApp.Models.HttpResult;
using ExpensesManagementApp.Models.Statistics;
using ExpensesManagementApp.Models.Transaction;

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

        public async Task<HttpResult<Models.Statistics.TransactionsChartSeries>> GetChartSeriesAsync(TransactionFilter filter)
        {
            try
            {
                var chartSeries = await _statisticsRepository.GetChartSeriesAsync(new DbTransactionFilter(filter));
                return new HttpResult<Models.Statistics.TransactionsChartSeries>(chartSeries);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] StatisticsService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Statistics.TransactionsChartSeries>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while attempt to initialize statistics: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Statistics.TransactionsChartSeries>();
            }
        }

        public async Task<HttpResult<Models.Statistics.StatisticsPackage>> GetStatisticsPackageAsync(TransactionFilter filter)
        {
            try
            {
                var statistics = await _statisticsRepository.InitializeStatisticsAsync();
                var chartSeries = await _statisticsRepository.GetChartSeriesAsync(new DbTransactionFilter(filter));

                var statisticsPackage = new StatisticsPackage
                {
                    Statistics = statistics,
                    TransactionsChartSeries = chartSeries
                };

                return new HttpResult<Models.Statistics.StatisticsPackage>(statisticsPackage);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] StatisticsService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Statistics.StatisticsPackage>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while attempt to initialize statistics: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Statistics.StatisticsPackage>();
            }
        }
    }
}
