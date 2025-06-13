using ExpensesManagementApp.Client.Services.CategoryService;
using ExpensesManagementApp.Logic.Repositories.CategoriesRepository;
using ExpensesManagementApp.Models.CustomExceptions;
using ExpensesManagementApp.Models.HttpResult;

namespace ExpensesManagementApp.Services
{
    public class CategoryService(ICategoriesRepository _categoriesRepository, ILogger<CategoryService> _logger) : ICategoryService
    {
        public async Task<HttpResult<Models.Category.Category>> GetCategoryAsync(Guid id)
        {
            try
            {
                return new HttpResult<Models.Category.Category>(await _categoriesRepository.GetCategoryAsync(id));
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Category.Category>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Category.Category>();
            }
        }

        public async Task<HttpResult<IEnumerable<Models.Category.Category>>> GetAllCategoriesAsync()
        {
            try
            {
                return new HttpResult<IEnumerable<Models.Category.Category>> (await _categoriesRepository.GetAllCategoriesAsync());
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.Category.Category>> (ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.Category.Category>>();
            }
        }

        public async Task<HttpResult<IEnumerable<Models.Category.Category>>> GetCategoriesBySearchString(string? searchString)
        {
            try
            {  
                return new HttpResult<IEnumerable<Models.Category.Category>>(await _categoriesRepository.GetCategoriesBySearchStringAsync(searchString ?? string.Empty));
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.Category.Category>>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.Category.Category>>();
            }
        }

        public async Task<HttpResult<Models.Category.Category>> UploadCategoryAsync(Models.Category.Category category)
        {
            try
            {
                return new HttpResult<Models.Category.Category>(await _categoriesRepository.UploadCategoryAsync(category));
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Category.Category>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.Category.Category>();
            }
        }

        public Task<HttpResult<Models.Category.Category>> UpdateCategoryAsync(Models.Category.Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<bool>> DeleteCategoryAsync(Guid id)
        {
            try
            {
                return new HttpResult<bool>(await _categoriesRepository.DeleteCategoryAsync(id));
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<bool>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] CategoriesService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<bool>();
            }
        }
    }
}
