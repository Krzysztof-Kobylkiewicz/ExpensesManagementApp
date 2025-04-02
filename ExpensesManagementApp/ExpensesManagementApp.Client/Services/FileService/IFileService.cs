using ExpensesManagementApp.Models.HttpResult;

namespace ExpensesManagementApp.Client.Services.FileService
{
    public interface IFileService
    {
        Task<HttpResult<Models.File.File?>> GetFileAsync(Guid id);
        Task<HttpResult<IEnumerable<Models.File.File?>>> GetAllFilesAsync();
        Task<HttpResult<Models.File.File?>> UploadFileAsync(Models.File.File file);
        Task<HttpResult<Models.File.File?>> UpdateFileAsync(Models.File.File file);
        Task<HttpResult<bool>> DeleteFileAsync(Guid id);
    }
}
