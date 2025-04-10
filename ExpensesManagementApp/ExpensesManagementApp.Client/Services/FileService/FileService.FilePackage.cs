using ExpensesManagementApp.Models.HttpResult;
using System.Net.Http.Json;

namespace ExpensesManagementApp.Client.Services.FileService
{
    public partial class FileService : IFileService
    {
        public async Task<HttpResult<Models.File.FilePackage?>> GetFilePackageAsync(Guid id)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                return await _httpClient.GetFromJsonAsync<HttpResult<Models.File.FilePackage?>>($"/api/v1/files/filePackage/{id}")
                    ?? new HttpResult<Models.File.FilePackage?>("No data found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while attepmt to get file package (GUID: {1ID}) through the API: {2M}", DateTime.Now, id, ex.Message);
                return new HttpResult<Models.File.FilePackage?>();
            }
        }

        public async Task<HttpResult<IEnumerable<Models.File.FilePackage?>>> GetAllFilepackagesAsync()
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                return await _httpClient.GetFromJsonAsync<HttpResult<IEnumerable<Models.File.FilePackage?>>>($"/api/v1/files/packages/all")
                    ?? new HttpResult<IEnumerable<Models.File.FilePackage?>>("No data found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while attepmt to get all file packages through the API: {2M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.File.FilePackage?>>();
            }
        }

        public async Task<HttpResult<bool>> DeleteFilePackageAsync(Guid id)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/v1/files/filePackage/delete/{id}");

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
                _logger.LogError(ex, "[{0D}] An error has occured while attempt to delete file package (GUID: {1ID}) through the API: {2M}", DateTime.Now, id, ex.Message);
                return new HttpResult<bool>();
            }
        }
    }
}
