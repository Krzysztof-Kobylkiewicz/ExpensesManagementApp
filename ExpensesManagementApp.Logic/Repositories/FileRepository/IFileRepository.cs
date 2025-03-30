namespace ExpensesManagementApp.Logic.Repositories.FileRepository
{
    internal interface IFileRepository
    {
        Task<Models.File.File?> GetFileAsync(Guid id);
        Task<Models.File.File?> UploadFileAsync(Models.File.File file);
        Task<Models.File.File?> UpdateFileAsync(Models.File.File file);
        Task<bool> DeleteFileAsync(Guid id);
    }
}
