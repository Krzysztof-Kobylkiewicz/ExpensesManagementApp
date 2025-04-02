using ExpensesManagementApp.Client.Services.FileService;
using ExpensesManagementApp.Logic.Repositories.FileRepository;
using ExpensesManagementApp.Models.CustomExceptions;
using ExpensesManagementApp.Models.HttpResult;

namespace ExpensesManagementApp.Services
{
    public class FileService(FileRepository _fileRepository, ILogger<FileService> _logger) : IFileService
    {

        public async Task<HttpResult<Models.File.File?>> GetFileAsync(Guid id)
        {
            try
            {
                var file = await _fileRepository.GetFileAsync(id);

                return new HttpResult<Models.File.File?>(file);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>();
            }
        }

        public async Task<HttpResult<IEnumerable<Models.File.File?>>> GetAllFilesAsync()
        {
            try
            {
                var files = await _fileRepository.GetAllFilesAsync();

                return new HttpResult<IEnumerable<Models.File.File?>>(files);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.File.File?>>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<IEnumerable<Models.File.File?>>();
            }
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
                _logger.LogError(ex, "[{0D}] FileService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>();
            }
        }

        public async Task<HttpResult<Models.File.File?>> UpdateFileAsync(Models.File.File file)
        {
            try
            {
                var _file = await _fileRepository.UpdateFileAsync(file);

                return new HttpResult<Models.File.File?>(_file);
            }
            catch (ExpensesManagementAppDbException ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an ExpensesManagementAppDbException: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>(ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{0D}] FileService threw an Exception: {1M}", DateTime.Now, ex.Message);
                return new HttpResult<Models.File.File?>();
            }
        }

        public async Task<HttpResult<bool>> DeleteFileAsync(Guid id)
        {
            try
            {
                var succes = await _fileRepository.DeleteFileAsync(id);

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
