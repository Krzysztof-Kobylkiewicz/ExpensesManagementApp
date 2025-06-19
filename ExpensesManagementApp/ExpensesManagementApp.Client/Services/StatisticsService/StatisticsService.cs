using ExpensesManagementApp.Models.Statistics;
using ExpensesManagementApp.Models.HttpResult;
using System.Net.Http.Json;
using ExpensesManagementApp.Models.Transaction;

namespace ExpensesManagementApp.Client.Services.StatisticsService
{
    public class StatisticsService(IHttpClientFactory _httpClientFactory, ILogger<StatisticsService> _logger) : IStatisticsService
    {
        public async Task<HttpResult<Statistics?>> InitializeStatisticsAsync()
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                return await _httpClient.GetFromJsonAsync<HttpResult<Statistics?>>("api/v1/statistics/initialize")
                    ?? new HttpResult<Statistics?>("No data found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while sending GET request: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Statistics?>();
            }
        }

        public async Task<HttpResult<Models.Statistics.TransactionsChartSeries>> GetChartSeriesAsync(TransactionFilter transactionFiltr)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var result = await _httpClient.PostAsJsonAsync<TransactionFilter>("api/v1/statistics/chartseries", transactionFiltr);

                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadFromJsonAsync<HttpResult<Models.Statistics.TransactionsChartSeries>>()
                        ?? new HttpResult<Models.Statistics.TransactionsChartSeries>("No data found.", 404); ;
                }
                 
                return new HttpResult<Models.Statistics.TransactionsChartSeries>(result.StatusCode.ToString(), 500);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while sending GET request for chart series: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Statistics.TransactionsChartSeries>();
            }
        }

        public async Task<HttpResult<Models.Statistics.StatisticsPackage>> GetStatisticsPackageAsync(TransactionFilter transactionFiltr)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var result = await _httpClient.PostAsJsonAsync<TransactionFilter>("api/v1/statistics/statisticspackage", transactionFiltr);

                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadFromJsonAsync<HttpResult<Models.Statistics.StatisticsPackage>>()
                        ?? new HttpResult<Models.Statistics.StatisticsPackage>("No data found.", 404); ;
                }

                return new HttpResult<Models.Statistics.StatisticsPackage>(result.StatusCode.ToString(), 500);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while sending GET request for statistics package: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Statistics.StatisticsPackage>();
            }
        }
    }
}
