using ExpensesManagementApp.Client.Services.CategoryService;
using ExpensesManagementApp.Logic.Repositories.CategoriesRepository;
using ExpensesManagementApp.Models.CustomExceptions;
using Core.Models;

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
            catch (Exception ex)
            {
                return ExceptionHandler<Models.Category.Category>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<IEnumerable<Models.Category.Category>>> GetAllCategoriesAsync()
        {
            try
            {
                return new HttpResult<IEnumerable<Models.Category.Category>> (await _categoriesRepository.GetAllCategoriesAsync());
            }
            catch (Exception ex)
            {
                return ExceptionHandler<IEnumerable<Models.Category.Category>>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<IEnumerable<Models.Category.Category>>> GetCategoriesBySearchString(string? searchString)
        {
            try
            {  
                return new HttpResult<IEnumerable<Models.Category.Category>>(await _categoriesRepository.GetCategoriesBySearchStringAsync(searchString ?? string.Empty));
            }
            catch (Exception ex)
            {
                return ExceptionHandler<IEnumerable<Models.Category.Category>>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<Models.Category.Category>> UploadCategoryAsync(Models.Category.Category category)
        {
            try
            {
                return new HttpResult<Models.Category.Category>(await _categoriesRepository.UploadCategoryAsync(category));
            }
            catch (Exception ex)
            {
                return ExceptionHandler<Models.Category.Category>.HandleExceptionAndLogError(ex, _logger);
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
            catch (Exception ex)
            {
                return ExceptionHandler<bool>.HandleExceptionAndLogError(ex, _logger);
            }
        }
    }
}
