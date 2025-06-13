namespace ExpensesManagementApp.Logic.Repositories.CategoriesRepository
{
    public interface ICategoriesRepository
    {
        Task<Models.Category.Category?> GetCategoryAsync(Guid id);
        Task<IEnumerable<Models.Category.Category?>> GetAllCategoriesAsync();
        Task<IEnumerable<Models.Category.Category?>> GetCategoriesBySearchStringAsync(string searchString);
        Task<Models.Category.Category> UploadCategoryAsync(Models.Category.Category category);
        Task<Models.Category.Category> UpdateCategoryAsync(Models.Category.Category category);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
