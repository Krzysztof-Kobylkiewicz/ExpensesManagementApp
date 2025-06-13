using ExpensesManagementApp.Database.DbModels;
using ExpensesManagementApp.Models.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpensesManagementApp.Logic.Repositories.FileRepository
{
    public partial class FileRepository : IFileRepository
    {
        public async Task<Models.File.FilePackage> GetFilePackageAsync(Guid id)
        {
            try
            {
                var file = await GetFileAsync(id);

                return await GetTransactionGroupsAssociatedWithFile(file);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while attempt to get file package: {1M}", DateTime.Now, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Models.File.FilePackage?>> GetAllFilePackagesAsync()
        {
            try
            {
                IList<Models.File.FilePackage?> filePackages = [];

                var files = await GetAllFilesAsync();

                foreach (var file in files)
                {
                    filePackages.Add(await GetTransactionGroupsAssociatedWithFile(file));
                }

                return filePackages;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while attempt to get all file packages: {1M}", DateTime.Now, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteFilePackageAsync(Guid id)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var fileToDelete = await GetEntityDTOAsync<Database.DbModels.File, Models.File.File, Guid>(id) ?? throw new ExpensesManagementAppDbException("No such file was found.", 404); //await database.Files.Include(f => f.Transactions).FirstOrDefaultAsync(f => f.Id == id) ?? throw new ExpensesManagementAppDbException("No such file was found.", 404);
                var fileTransactionGroupIds = fileToDelete.Transactions?.Where(t => t.TransactionGroupId.HasValue).Select(t => t.TransactionGroupId) ?? [];
                var transactionGroupsToDelete = await GetEntitiesDTOAsync<TransactionGroup, Models.Transaction.TransactionGroup, Guid>(transactionGroup => transactionGroup.Where(tg => fileTransactionGroupIds.Contains(tg.Id)));


                await DeleteEntityAsync<Database.DbModels.File, Models.File.File, Guid>(id);

                foreach (var transactionGroup in transactionGroupsToDelete)
                {
                    if (transactionGroup != null)
                    {
                        await transactionsRepository.DeleteTransactionGroupAsync(transactionGroup.Id);
                    }
                }

                await SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while attempt to remove file: {1M}", DateTime.Now, ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
