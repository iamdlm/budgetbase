using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BudgetBase.Web.Razor.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int? statusCode = null)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            // If statusCode is not provided, try to get it from the HttpContext
            StatusCode = statusCode ?? HttpContext.Response.StatusCode;

            if (StatusCode == 0)
            {
                // If we still don't have a valid status code, default to 500
                StatusCode = 500;
            }

            ErrorMessage = GetErrorMessage(StatusCode);

            // Ensure the correct status code is set on the response
            HttpContext.Response.StatusCode = StatusCode;

            // Log the error with structured information
            var eventId = new EventId(StatusCode, "ErrorPage");
            _logger.LogError(eventId, "Error {StatusCode} occurred. RequestId: {RequestId}. Message: {ErrorMessage}",
                             StatusCode, RequestId, ErrorMessage);
        }

        private string GetErrorMessage(int statusCode)
        {
            return statusCode switch
            {
                404 => "The page you're looking for doesn't exist.",
                500 => "An internal server error occurred.",
                _ => "An error occurred while processing your request."
            };
        }
    }
}