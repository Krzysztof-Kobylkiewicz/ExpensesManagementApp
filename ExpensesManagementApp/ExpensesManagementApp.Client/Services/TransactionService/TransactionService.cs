using ExpensesManagementApp.Models.HttpResult;
using ExpensesManagementApp.Models.Transaction;
using System.Net.Http.Json;

namespace ExpensesManagementApp.Client.Services.TransactionService
{
    public class TransactionService(IHttpClientFactory _httpClientFactory, ILogger<TransactionService> _logger) : ITransactionService
    {
        public async Task<HttpResult<Transaction>> GetTransactionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<IEnumerable<Transaction>>> GetAllTransactionsAsync()
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.GetAsync("/api/v1/transactions/get/all");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<HttpResult<IEnumerable<Transaction>>>()
                        ?? new HttpResult<IEnumerable<Transaction>>("No data found.", 404);
                }
                else
                {
                    return new HttpResult<IEnumerable<Transaction>>(response?.RequestMessage?.ToString(), (int?)response?.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while attempt to get all transactions through the API: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Transaction>>();
            }
        }

        public async Task<HttpResult<bool>> DeleteTransactionAsync(Guid id)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/v1/transactions/delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<HttpResult<bool>>()
                        ?? new HttpResult<bool>("No data found.", 404);
                }
                else
                {
                    return new HttpResult<bool>(response?.RequestMessage?.ToString(), (int?)response?.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while attempt to delete transaction (GUID: {1ID}) through the API: {2M}", DateTime.Now, id, ex.Message);
                return new HttpResult<bool>();
            }
        }

        public async Task<HttpResult<TransactionGroup>> AssignTransactionsToGroup(Models.Transaction.TransactionGroup transactionGroup)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1/transactions/assignToGroup", transactionGroup);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<HttpResult<TransactionGroup>>()
                        ?? new HttpResult<TransactionGroup>("No data found.", 404);
                }
                else
                {
                    return new HttpResult<TransactionGroup>(response?.RequestMessage?.ToString(), (int?)response?.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while attempt to assign transactions to group through the API: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<TransactionGroup>();
            }
        }
    }
}
