using ExpensesManagementApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ExpensesManagementApp.Models.CustomExceptions;
using Core.Repositories;
using ExpensesManagementApp.Database.DbModels;
using ExpensesManagementApp.Database.Filters;

namespace ExpensesManagementApp.Logic.Repositories.CategoriesRepository
{
    public class CategoriesRepository : Repository<ApplicationDbContext, Category, Models.Category.Category, Guid>, ICategoriesRepository
    {
        public CategoriesRepository(ApplicationDbContext dbContext, ILogger<Category> logger) : base(dbContext, logger) { }

        public async Task<Models.Category.Category?> GetCategoryAsync(Guid id) => await GetEntityDTOAsync<Category, Models.Category.Category, Guid >(id);

        public async Task<IEnumerable<Models.Category.Category?>> GetAllCategoriesAsync() => await GetEntitiesDTOAsync<Category, Models.Category.Category, Guid>(c => c);

        public async Task<IEnumerable<Models.Category.Category?>> GetCategoriesBySearchStringAsync(string searchString) => await GetFilteredCategoriesAsync(new DbCategoryFilter { SearchString = searchString});

        public async Task<IEnumerable<Models.Category.Category?>> GetFilteredCategoriesAsync(DbCategoryFilter filter) => await GetFilteredEntiteiesDTOAsync<Category, Models.Category.Category, Guid, DbCategoryFilter>(filter, c => c);

        public async Task<Models.Category.Category> UploadCategoryAsync(Models.Category.Category category) => await AddEntityDTOAsync<Category, Models.Category.Category, Guid>(category);

        public Task<Models.Category.Category> UpdateCategoryAsync(Models.Category.Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            using var dbtransaction = _dbContext.Database.BeginTransaction();

            try
            {
                var categoryToDelete = await _dbContext.Categories.Where(c => c.Id == id).FirstOrDefaultAsync()
                    ?? throw new ExpensesManagementAppDbException("No such category was found.", 404);

                _dbContext.Categories.Remove(categoryToDelete);
                int n = await _dbContext.SaveChangesAsync();

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
