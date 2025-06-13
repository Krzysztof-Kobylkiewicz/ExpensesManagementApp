using ExpensesManagementApp.Client.Services.StatisticsService;

namespace ExpensesManagementApp.MapGroup
{
    internal static class StatisticsEndpointRoutBuilderExtensions
    {
        internal static IEndpointConventionBuilder MapStatistics(this IEndpointRouteBuilder endpoints)
        {
            var statisticsGroup = endpoints.MapGroup("api/v1/statistics");

            // endpoints

            statisticsGroup.MapGet("/initialize", (IStatisticsService statisticsService) => statisticsService.InitializeStatisticsAsync());

            statisticsGroup.MapPost("/chartseries", (IStatisticsService statisticsService, Models.Transaction.TransactionFilter transactionFiltr) => statisticsService.GetChartSeriesAsync(transactionFiltr));

            statisticsGroup.MapPost("/statisticspackage", (IStatisticsService statisticsService, Models.Transaction.TransactionFilter transactionFiltr) => statisticsService.GetStatisticsPackageAsync(transactionFiltr));

            return statisticsGroup;
        }
    }
}
