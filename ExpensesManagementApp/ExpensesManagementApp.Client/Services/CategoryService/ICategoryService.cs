using ExpensesManagementApp.Models.HttpResult;

namespace ExpensesManagementApp.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<HttpResult<Models.Category.Category>> GetCategoryAsync(Guid id);
        Task<HttpResult<IEnumerable<Models.Category.Category>>> GetAllCategoriesAsync();
        Task<HttpResult<IEnumerable<Models.Category.Category>>> GetCategoriesBySearchString(string? searchString);
        Task<HttpResult<Models.Category.Category>> UploadCategoryAsync(Models.Category.Category category);
        Task<HttpResult<Models.Category.Category>> UpdateCategoryAsync(Models.Category.Category category);
        Task<HttpResult<bool>> DeleteCategoryAsync(Guid id);
    }
}