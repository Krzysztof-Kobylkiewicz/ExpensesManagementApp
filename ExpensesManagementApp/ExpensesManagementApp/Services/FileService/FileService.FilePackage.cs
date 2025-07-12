using ExpensesManagementApp.Client.Services.FileService;
using ExpensesManagementApp.Models.CustomExceptions;
using Core.Models;

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
            catch (Exception ex)
            {
                return ExceptionHandler<Models.File.FilePackage?>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<IEnumerable<Models.File.FilePackage?>>> GetAllFilepackagesAsync()
        {
            try
            {
                var filePackages = await _fileRepository.GetAllFilePackagesAsync();

                return new HttpResult<IEnumerable<Models.File.FilePackage?>>(filePackages);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<IEnumerable<Models.File.FilePackage?>>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<bool>> DeleteFilePackageAsync(Guid id)
        {
            try
            {
                var succes = await _fileRepository.DeleteFilePackageAsync(id);

                return new HttpResult<bool>(succes);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<bool>.HandleExceptionAndLogError(ex, _logger);
            }
        }
    }
}
