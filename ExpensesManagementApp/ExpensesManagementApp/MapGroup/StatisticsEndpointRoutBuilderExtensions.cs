using ExpensesManagementApp.Logic.Repositories;

namespace ExpensesManagementApp.MapGroup
{
    internal static class StatisticsEndpointRoutBuilderExtensions
    {
        internal static IEndpointConventionBuilder MapStatistics(this IEndpointRouteBuilder endpoints)
        {
            var statisticsGroup = endpoints.MapGroup("");
            statisticsGroup.RequireAuthorization();

            // endpoints

            statisticsGroup.MapGet("/initialize", (StatisticsRepository statisticsRepository) => statisticsRepository.InitializeAsync());

            return statisticsGroup;
        }
    }
}
