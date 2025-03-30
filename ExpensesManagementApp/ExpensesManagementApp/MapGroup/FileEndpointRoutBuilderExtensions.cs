namespace ExpensesManagementApp.MapGroup
{
    internal static class FileEndpointRoutBuilderExtensions
    {
        internal static IEndpointConventionBuilder MapFiles(this IEndpointRouteBuilder endpoints)
        {
            var fileGroup = endpoints.MapGroup("");

            fileGroup.MapPost("/upload", (Models.File.File file, Client.Services.IFileService fileService) => fileService.UploadFileAsync(file));

            return fileGroup;
        }
    }
}
