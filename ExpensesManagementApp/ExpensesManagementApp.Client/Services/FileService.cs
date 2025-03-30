using ExpensesManagementApp.Models.HttpResult;
using System.Net.Http.Json;

namespace ExpensesManagementApp.Client.Services
{
    public class FileService(IHttpClientFactory _httpClientFactory, ILogger<FileService> _logger) : IFileService
    {
        public async Task<HttpResult<Models.File.File?>> GetFileAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<Models.File.File?>> UploadFileAsync(Models.File.File file)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1/files/upload", file);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<HttpResult<Models.File.File?>>() ?? new HttpResult<Models.File.File?>();
                }
                else
                {
                    return new HttpResult<Models.File.File?>(response?.RequestMessage?.ToString(), (int?) response?.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while trying to send file through the api: {1M}", DateTime.Now, ex.Message);
                throw new HttpRequestException(ex.Message, ex, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResult<Models.File.File?>> UpdateFileAsync(Models.File.File file)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<bool>> DeleteFileAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
