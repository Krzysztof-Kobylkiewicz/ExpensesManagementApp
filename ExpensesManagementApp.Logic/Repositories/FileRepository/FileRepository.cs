using ExpensesManagementApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ExpensesManagementApp.Models.CustomExceptions;

namespace ExpensesManagementApp.Logic.Repositories.FileRepository
{
    public class FileRepository(ApplicationDbContext database, ILogger<FileRepository> logger) : IFileRepository
    {
        public async Task<Models.File.File?> GetFileAsync(Guid id)
        {
            var dbFile = await database.Files.Include(f => f.Expenses).FirstOrDefaultAsync(f => f.FileId == id) ?? throw new ExpensesManagementAppDbException("No file with given id was found", 404);

            return Database.DbModels.File.ConvertToFileDTO(dbFile);
        }

        public async Task<IEnumerable<Models.File.File?>> GetAllFilesAsync()
        {
            var dbFiles = await database.Files.Include(f => f.Expenses).ToListAsync() ?? throw new ExpensesManagementAppDbException("No files were found.", 404);

            return dbFiles.Select(f => Database.DbModels.File.ConvertToFileDTO(f));
        }

        public async Task<Models.File.File?> UploadFileAsync(Models.File.File file)
        {
            using var transaction = database.Database.BeginTransaction();
            try
            {
                var matchingDbFiles = database.Files.Where(f => f.FileName != null && f.FileName.Equals(file.FileName) && f.FileSize == file.FileSize && f.BankType.Equals(file.BankType) && f.Expenses != null && f.Expenses.Count == file.Expenses.Count());

                if (matchingDbFiles.Count() > 0)
                {
                    throw new ExpensesManagementAppDbException("This file has been already uploaded.", 409);
                }

                file.FileId = file.FileId == Guid.Empty ? Guid.NewGuid() : file.FileId;

                var dbFile = Database.DbModels.File.ConvertToDbFile(file);

                await database.AddAsync(dbFile);
                await database.SaveChangesAsync();
                await transaction.CommitAsync();
                return file;
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Błąd podczas zapisywania pliku: {1M}", DateTime.Now, ex.Message);
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Models.File.File?> UpdateFileAsync(Models.File.File file)
        {
            using var transaction = database.Database.BeginTransaction();
            try
            {
                var fileToUpdate = await database.Files.FirstOrDefaultAsync(f => f.FileId == file.FileId) ?? throw new ExpensesManagementAppDbException("No such file was found.", 404);

                fileToUpdate.UpdateFile(file);

                await database.SaveChangesAsync();
                await transaction.CommitAsync();

                return Database.DbModels.File.ConvertToFileDTO(fileToUpdate);
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Błąd podczas zapisywania pliku: {1M}", DateTime.Now, ex.Message);
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> DeleteFileAsync(Guid id)
        {
            using var transaction = database.Database.BeginTransaction();
            try
            {
                var fileToDelete = await database.Files.FirstOrDefaultAsync(f => f.FileId == id) ?? throw new ExpensesManagementAppDbException("No such file was found.", 404);

                database.Files.Remove(fileToDelete);
                int n = await database.SaveChangesAsync();

                if (n > 1)
                    throw new Exception("An attempt to delete more than one file by id was registered.");

                await transaction.CommitAsync();

                return n == 1;
            }
            catch (Exception ex)
            {
                logger.LogError("[{0D}] Błąd podczas usuwania pliku: {1M}", DateTime.Now, ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
