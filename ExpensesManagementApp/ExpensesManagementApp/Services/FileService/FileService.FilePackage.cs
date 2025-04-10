using ExpensesManagementApp.Client.Services.FileService;
using ExpensesManagementApp.Models.CustomExceptions;
using ExpensesManagementApp.Models.HttpResult;

namespace ExpensesManagementApp.Services.FileService
{
    public partial class FileService : IFileService
    {
        public async Task<HttpResult<Models.File.FilePackage?>> GetFilePackageAsync(Guid id)
        {
            try
            {
                var file = await _fileRepository.GetFilePackageAsync(id);

                return new HttpResult<Models.File.FilePackage?>(file);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.FilePackage?>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.FilePackage?>();
            }
        }

        public async Task<HttpResult<IEnumerable<Models.File.FilePackage?>>> GetAllFilepackagesAsync()
        {
            try
            {
                var filePackages = await _fileRepository.GetAllFilePackagesAsync();

                return new HttpResult<IEnumerable<Models.File.FilePackage?>>(filePackages);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.File.FilePackage?>>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.File.FilePackage?>>();
            }
        }

        public async Task<HttpResult<bool>> DeleteFilePackageAsync(Guid id)
        {
            try
            {
                var succes = await _fileRepository.DeleteFilePackageAsync(id);

                return new HttpResult<bool>(succes);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<bool>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<bool>();
            }
        }
    }
}
