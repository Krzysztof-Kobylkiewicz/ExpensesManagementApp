using ExpensesManagementApp.Models.Statistics;
using ExpensesManagementApp.Models.HttpResult;
using System.Net.Http.Json;

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
    }
}
