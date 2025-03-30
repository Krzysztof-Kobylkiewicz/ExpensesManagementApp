using ExpensesManagementApp.Client.Services;
using ExpensesManagementApp.Logic.Repositories.FileRepository;
using ExpensesManagementApp.Models.CustomExceptions;
using ExpensesManagementApp.Models.HttpResult;

namespace ExpensesManagementApp.Services
{
    public class FileService(FileRepository _fileRepository, ILogger<FileService> _logger) : IFileService
    {

        public async Task<HttpResult<Models.File.File?>> GetFileAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<Models.File.File?>> UploadFileAsync(Models.File.File file)
        {
            try
            {
                var _file = await _fileRepository.UploadFileAsync(file);

                return new HttpResult<Models.File.File?>(_file);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>();
            }
        }

        public async Task<HttpResult<Models.File.File?>> UpdateFileAsync(Models.File.File file)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResult<bool>> DeleteFileAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
