using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetBase.Web.Razor.Helpers
{
    public static class ModelStateExtensions
    {
        public static void AddValidationErrorsToModelState(this ModelStateDictionary modelState, IDictionary<string, string[]> errors)
        {
            foreach (var error in errors)
            {
                if (string.IsNullOrEmpty(error.Key))
                {
                    foreach (var errorMessage in error.Value)
                    {
                        modelState.AddModelError(string.Empty, errorMessage);
                    }
                }
                else
                {
                    foreach (var errorMessage in error.Value)
                    {
                        modelState.AddModelError(error.Key, errorMessage);
                    }
                }
            }
        }
    }
}
