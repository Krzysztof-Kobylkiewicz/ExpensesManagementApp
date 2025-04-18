using ExpensesManagementApp.Models.Category;
using ExpensesManagementApp.Models.HttpResult;
using System.Net.Http.Json;

namespace ExpensesManagementApp.Client.Services.CategoryService
{
    public class CategoryService(IHttpClientFactory _httpClientFactory, ILogger<CategoryService> _logger) : ICategoryService
    {
        public async Task<HttpResult<Category>> GetCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<IEnumerable<Category>>> GetAllCategoriesAsync()
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                return await _httpClient.GetFromJsonAsync<HttpResult<IEnumerable<Category>>>("/api/v1/categories/all")
                    ?? new HttpResult<IEnumerable<Category>>("No data found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while retrieving categories through the API: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Category>>();
            }
        }

        public async Task<HttpResult<IEnumerable<Models.Category.Category>>> GetCategoriesBySearchString(string? searchString)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                return await _httpClient.GetFromJsonAsync<HttpResult<IEnumerable<Models.Category.Category>>>($"/api/v1/categories/{searchString}")
                    ?? new HttpResult<IEnumerable<Models.Category.Category>>("No data found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while retrieving categories by search string through the API: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.Category.Category>>();
            }
        }

        public async Task<HttpResult<Models.Category.Category>> UploadCategoryAsync(Models.Category.Category category)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1/categories/add", category);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<HttpResult<Models.Category.Category>>()
                        ?? new HttpResult<Models.Category.Category>("No data found.", 404);
                }
                else
                {
                    return new HttpResult<Models.Category.Category>(response?.RequestMessage?.ToString(), (int?)response?.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] An error has occured while uploading category through the API: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Category.Category>();
            }
        }

        public async Task<HttpResult<Category>> UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<bool>> DeleteCategoryAsync(Guid id)
        {
            using var _httpClient = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/v1/categories/delete/{id}");

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
                _logger.LogError(ex, "[{0D}] An error has occured while deleting category through the API: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<bool>(false);
            }
        }
    }
}
