using ExpensesManagementApp.Models.HttpResult;
using System.Net.Http.Json;

namespace ExpensesManagementApp.Client.Services.FileService
{
    public partial class FileService(IHttpClientFactory _httpClientFactory, ILogger<FileService> _logger) : IFileService
    {
        public async Task<HttpResult<Models.File.File?>> GetFileAsync(Guid id)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                return await _httpClient.GetFromJsonAsync<HttpResult<Models.File.File?>>($"/api/v1/files/file/{id}")
                    ?? new HttpResult<Models.File.File?>("No data found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while attepmt to get file (GUID: {1ID}) through the API: {2M}", DateTime.Now, id, ex.Message);
                return new HttpResult<Models.File.File?>();
            }
        }

        public async Task<HttpResult<IEnumerable<Models.File.File?>>> GetAllFilesAsync()
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                return await _httpClient.GetFromJsonAsync<HttpResult<IEnumerable<Models.File.File?>>>("/api/v1/files/all")
                    ?? new HttpResult<IEnumerable<Models.File.File?>>("No data found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while attepmt to get all files through the API: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.File.File?>>();
            }
        }

        public async Task<HttpResult<Models.File.File?>> UploadFileAsync(Models.File.File file)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1/files/upload", file);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<HttpResult<Models.File.File?>>()
                        ?? new HttpResult<Models.File.File?>("No data found.", 404);
                }
                else
                {
                    return new HttpResult<Models.File.File?>(response?.RequestMessage?.ToString(), (int?)response?.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while sending file through the API: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>();
            }
        }

        public async Task<HttpResult<Models.File.File?>> UpdateFileAsync(Models.File.File file)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/v1/files/update/{file.FileId}", file);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<HttpResult<Models.File.File?>>()
                        ?? new HttpResult<Models.File.File?>("No data found.", 404);
                }
                else
                {
                    return new HttpResult<Models.File.File?>(response?.RequestMessage?.ToString(), (int?)response?.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while attempt to update file (GUID: {1ID}) through the API: {2M}", DateTime.Now, file.FileId, ex.Message);
                return new HttpResult<Models.File.File?>();
            }
        }

        public async Task<HttpResult<bool>> DeleteFileAsync(Guid id)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/v1/files/delete/{id}");

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
                _logger.LogError(ex, "[{0D}] An error has occured while attempt to delete file (GUID: {1ID}) through the API: {2M}", DateTime.Now, id, ex.Message);
                return new HttpResult<bool>();
            }
        }
    }
}
