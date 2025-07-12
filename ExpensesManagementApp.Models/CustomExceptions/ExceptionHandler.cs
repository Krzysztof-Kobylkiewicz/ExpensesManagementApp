using Core.Models;
using Microsoft.Extensions.Logging;

namespace ExpensesManagementApp.Models.CustomExceptions
{
    public static class ExceptionHandler<T>
    {
        private const string ExceptionMessage = "Internal server error";

        public static HttpResult<T> HandleExceptionAndLogError(Exception ex, ILogger logger)
        {
            logger.LogError(ex, "[{0D}] Exception handler caught the error: {1M}", DateTime.Now, ex.Message);
            return HandleException(ex);
        }

        public static HttpResult<T> HandleException(Exception ex) => (ex) switch
        {
            ExpensesManagementAppDbException => new HttpResult<T>(ex.Message, 500),
            Exception => new HttpResult<T>(ExceptionMessage, 500),
            _ => throw new NotImplementedException()
        };
    }
}
