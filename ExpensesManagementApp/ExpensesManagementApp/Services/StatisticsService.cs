using ExpensesManagementApp.Client.Services.StatisticsService;
using ExpensesManagementApp.Database.Filters;
using ExpensesManagementApp.Logic.Repositories;
using ExpensesManagementApp.Models.CustomExceptions;
using Core.Models;
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
                var statistics = await _statisticsRepository.InitializeStatisticsAsync(new DbTransactionFilter());

                return new HttpResult<Statistics?>(statistics);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<Statistics?>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<Models.Statistics.TransactionsChartSeries>> GetChartSeriesAsync(TransactionFilter filter)
        {
            try
            {
                var chartSeries = await _statisticsRepository.GetChartSeriesAsync(new DbTransactionFilter(filter));
                return new HttpResult<Models.Statistics.TransactionsChartSeries>(chartSeries);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TransactionsChartSeries>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<Models.Statistics.StatisticsPackage>> GetStatisticsPackageAsync(TransactionFilter filter)
        {
            try
            {
                var statisticsPackage = new StatisticsPackage
                {
                    Statistics = await _statisticsRepository.InitializeStatisticsAsync(new DbTransactionFilter(filter)),
                    TransactionsChartSeries = await _statisticsRepository.GetChartSeriesAsync(new DbTransactionFilter(filter)),
                    LatestTransactionDate = await _statisticsRepository.LatestTransactionDateAsync(),
                    EarliestTransactionDate = await _statisticsRepository.EarliestTransactionDateAsync()
                };

                return new HttpResult<Models.Statistics.StatisticsPackage>(statisticsPackage);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<StatisticsPackage>.HandleExceptionAndLogError(ex, _logger);
            }
        }
    }
}
