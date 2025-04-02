using ExpensesManagementApp.Client.Services.FileService;

namespace ExpensesManagementApp.MapGroup
{
    internal static class FileEndpointRoutBuilderExtensions
    {
        internal static IEndpointConventionBuilder MapFiles(this IEndpointRouteBuilder endpoints)
        {
            var fileGroup = endpoints.MapGroup("");

            fileGroup.MapGet("/file/{id:guid}", (IFileService fileService, Guid id) => fileService.GetFileAsync(id));
            fileGroup.MapGet("/all", (IFileService fileService) => fileService.GetAllFilesAsync());

            fileGroup.MapPost("/upload", (Models.File.File file, IFileService fileService) => fileService.UploadFileAsync(file));

            fileGroup.MapPut("/update/{id:guid}", (Models.File.File file, IFileService fileService) => fileService.UpdateFileAsync(file));

            fileGroup.MapDelete("/delete/{id:guid}", (Guid id, IFileService fileService) => fileService.DeleteFileAsync(id));

            return fileGroup;
        }
    }
}
