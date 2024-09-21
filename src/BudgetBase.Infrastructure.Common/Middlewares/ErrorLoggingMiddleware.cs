using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BudgetBase.Infrastructure.Common.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleExceptionLogging(ex, context);
                throw;
            }
        }

        /// <summary>
        /// Handles exception logging by mapping the exception to an appropriate EventId and EventName.
        /// </summary>
        private void HandleExceptionLogging(Exception ex, HttpContext context)
        {
            var requestId = Activity.Current?.Id ?? context.TraceIdentifier;
            var (eventId, eventName) = MapExceptionToEvent(ex);

            // Log the error with the mapped EventId and EventName
            _logger.LogError(eventId, ex, $"Event: {eventName}, RequestId: {requestId}, Message: {ex.Message}");
        }

        /// <summary>
        /// Maps specific exceptions to corresponding EventId and EventName.
        /// </summary>
        private (EventId eventId, string eventName) MapExceptionToEvent(Exception ex)
        {
            return ex switch
            {
                UnauthorizedAccessException => (new EventId(1002, "UnauthorizedAccess"), "Unauthorized Access"),
                NullReferenceException => (new EventId(1003, "NullReference"), "Null Reference Exception"),
                ArgumentException => (new EventId(1004, "ArgumentError"), "Argument Exception"),
                InvalidOperationException => (new EventId(1005, "InvalidOperation"), "Invalid Operation"),
                TimeoutException => (new EventId(1006, "TimeoutError"), "Operation Timed Out"),
                _ => (new EventId(1001, "UnhandledException"), "Unhandled Exception")
            };
        }
    }
}
