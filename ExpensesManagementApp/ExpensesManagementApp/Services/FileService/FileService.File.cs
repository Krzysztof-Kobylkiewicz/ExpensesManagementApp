using ExpensesManagementApp.Client.Services.FileService;
using ExpensesManagementApp.Logic.Repositories.FileRepository;
using ExpensesManagementApp.Models.CustomExceptions;
using Core.Models;
using ExpensesManagementApp.Models.Statistics;

namespace ExpensesManagementApp.Services.FileService
{
    public partial class FileService(IFileRepository _fileRepository, ILogger<FileService> _logger) : IFileService
    {

        public async Task<HttpResult<Models.File.File?>> GetFileAsync(Guid id)
        {
            try
            {
                var file = await _fileRepository.GetFileAsync(id);

                return new HttpResult<Models.File.File?>(file);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<Models.File.File?>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<IEnumerable<Models.File.File?>>> GetAllFilesAsync()
        {
            try
            {
                var files = await _fileRepository.GetAllFilesAsync();

                return new HttpResult<IEnumerable<Models.File.File?>>(files);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<IEnumerable<Models.File.File?>>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<Models.File.File?>> UploadFileAsync(Models.File.File file)
        {
            try
            {
                file.Transactions = Models.File.File.AssignSenderOrRecipent(file.Transactions);

                var _file = await _fileRepository.UploadFileAsync(file);

                return new HttpResult<Models.File.File?>(_file);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<Models.File.File?>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<Models.File.File?>> UpdateFileAsync(Models.File.File file)
        {
            try
            {
                var _file = await _fileRepository.UpdateFileAsync(file);

                return new HttpResult<Models.File.File?>(_file);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<Models.File.File?>.HandleExceptionAndLogError(ex, _logger);
            }
        }

        public async Task<HttpResult<bool>> DeleteFileAsync(Guid id)
        {
            try
            {
                var succes = await _fileRepository.DeleteFileAsync(id);

                return new HttpResult<bool>(succes);
            }
            catch (Exception ex)
            {
                return ExceptionHandler<bool>.HandleExceptionAndLogError(ex, _logger);
            }
        }
    }
}
