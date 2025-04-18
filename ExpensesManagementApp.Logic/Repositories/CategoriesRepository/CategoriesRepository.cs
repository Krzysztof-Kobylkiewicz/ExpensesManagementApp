using ExpensesManagementApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ExpensesManagementApp.Models.CustomExceptions;

namespace ExpensesManagementApp.Logic.Repositories.CategoriesRepository
{
    public class CategoriesRepository(ApplicationDbContext database, ILogger<CategoriesRepository> _logger) : ICategoriesRepository
    {
        public Task<Models.Category.Category> GetCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Models.Category.Category>> GetAllCategoriesAsync()
        {
            return await database.Categories.Select(c => Database.DbModels.Category.ConvertToCategoryDTO(c)).ToArrayAsync();
        }

        public async Task<IEnumerable<Models.Category.Category>> GetCategoriesBySearchStringAsync(string searchString)
        {
            //todo czy mozna stworzyc jedno query i potem wykonać je inaczej w zależności od parametru

            //if (string.IsNullOrEmpty(searchString))
            //{
                return await database.Categories.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.ToLower().Contains(searchString.ToLower())).Select(c => Database.DbModels.Category.ConvertToCategoryDTO(c)).Take(10).ToArrayAsync();
            //}
            //else
            //{
            //    return await database.Categories.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.ToLower().Contains(searchString.ToLower())).Select(c => Database.DbModels.Category.ConvertToCategoryDTO(c)).ToArrayAsync();
            //}
        }

        public async Task<Models.Category.Category> UploadCategoryAsync(Models.Category.Category category)
        {
            using var transaction = database.Database.BeginTransaction();
            try
            {
                var dbCategory = Database.DbModels.Category.ConvertToDbCategory(category);

                dbCategory.Id = Guid.NewGuid();
                dbCategory.UpoloadDate = DateTime.Now;

                await database.AddAsync(dbCategory);
                await database.SaveChangesAsync();
                await transaction.CommitAsync();

                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] Error while attempt to upload category: {1M}", DateTime.Now, ex.Message);
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task<Models.Category.Category> UpdateCategoryAsync(Models.Category.Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            using var dbtransaction = database.Database.BeginTransaction();
            try
            {
                var categoryToDelete = await database.Categories.Where(c => c.Id == id).FirstOrDefaultAsync()
                    ?? throw new ExpensesManagementAppDbException("No such category was found.", 404);
                
                database.Categories.Remove(categoryToDelete);
                int n = await database.SaveChangesAsync();

                await dbtransaction.CommitAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] Error while attempt to remove category: {1M}", DateTime.Now, ex.Message);
                await dbtransaction.RollbackAsync();
                throw;
            }
        }
    }
}
