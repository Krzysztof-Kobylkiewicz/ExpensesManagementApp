namespace ExpensesManagementApp.Logic.Repositories.FileRepository
{
    public interface IFileRepository
    {
        Task<Models.File.File?> GetFileAsync(Guid id);
        Task<IEnumerable<Models.File.File?>> GetAllFilesAsync();
        Task<Models.File.FilePackage> GetFilePackageAsync(Guid id);
        Task<IEnumerable<Models.File.FilePackage?>> GetAllFilePackagesAsync();
        Task<Models.File.File?> UploadFileAsync(Models.File.File file);
        Task<Models.File.File?> UpdateFileAsync(Models.File.File file);
        Task<bool> DeleteFileAsync(Guid id);
    }
}
