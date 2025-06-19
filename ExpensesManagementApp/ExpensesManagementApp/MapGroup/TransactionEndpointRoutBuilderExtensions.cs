using ExpensesManagementApp.Client.Services.TransactionService;

namespace ExpensesManagementApp.MapGroup
{
    internal static class TransactionEndpointRoutBuilderExtensions
    {
        internal static IEndpointConventionBuilder MapTransactions(this IEndpointRouteBuilder endpoints)
        {
            var transactionsGroup = endpoints.MapGroup("api/v1/transactions");

            // endpoints

            transactionsGroup.MapGet("/get/all", (ITransactionService transactionService) => transactionService.GetAllTransactionsAsync());

            transactionsGroup.MapPost("/get/filter", (ITransactionService transactionService, Models.Transaction.TransactionFilter filter) => transactionService.GetSpecificTransactionsAsync(filter));

            transactionsGroup.MapDelete("/delete/{id:guid}", (ITransactionService transactionService, Guid id) => transactionService.DeleteTransactionAsync(id));

            transactionsGroup.MapPost("/assignToGroup", (ITransactionService transactionService, Models.Transaction.TransactionGroup transactionGroup) => transactionService.AssignTransactionsToGroup(transactionGroup));

            return transactionsGroup;
        }
    }
}
