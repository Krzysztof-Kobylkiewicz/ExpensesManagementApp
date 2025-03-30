using ExpensesManagementApp.Models.Statistics;

namespace ExpensesManagementApp.Client.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IConfiguration _configuration;
        //private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public StatisticsService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<StatisticsService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            //_httpClient = httpClientFactory.CreateClient();
            //_httpClient.BaseAddress = new Uri(_configuration["BaseUri"]);
        }

        public async Task<Statistics?> InitializeAsync()
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.GetAsync("api/v1/statistics/initialize");

                if (response.IsSuccessStatusCode)
                {
                    var statistics = await response.Content.ReadAsStringAsync();  /*ReadFromJsonAsync<Statistics?>();*/

                    return null;
                }

                throw new Exception("No data found");
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while sending GET request: {1M}", DateTime.Now, ex.Message);
                throw;
            }
        }
    }
}
