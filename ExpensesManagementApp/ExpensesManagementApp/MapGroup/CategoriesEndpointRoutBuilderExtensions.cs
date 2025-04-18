using ExpensesManagementApp.Client.Services.CategoryService;

namespace ExpensesManagementApp.MapGroup
{
    internal static class CategoriesEndpointRoutBuilderExtensions
    {
        internal static IEndpointConventionBuilder MapCategories(this IEndpointRouteBuilder endpoints)
        {
            var categoriesGroup = endpoints.MapGroup("api/v1/categories");

            categoriesGroup.MapGet("/all", (ICategoryService categoriesService) => categoriesService.GetAllCategoriesAsync());

            categoriesGroup.MapGet("/{serachString}", (ICategoryService categoriesService, string serachString) => categoriesService.GetCategoriesBySearchString(serachString));

            categoriesGroup.MapPost("/add", (ICategoryService categoriesService, Models.Category.Category category) => categoriesService.UploadCategoryAsync(category));

            categoriesGroup.MapDelete("/delete/{id:guid}", (ICategoryService categoriesService, Guid id) => categoriesService.DeleteCategoryAsync(id));

            return categoriesGroup;
        }
    }
}
