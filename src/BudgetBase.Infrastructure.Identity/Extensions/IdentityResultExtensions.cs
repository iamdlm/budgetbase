using BudgetBase.Core.Application.DTOs.Application;
using Microsoft.AspNetCore.Identity;

namespace BudgetBase.Infrastructure.Identity.Helpers
{
    public static class IdentityResultExtensions
    {
        public static ApplicationResponse ToAuthenticationResult(this IdentityResult result)
        {
            return new ApplicationResponse()
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.ToDictionary(e => e.Code, e => e.Description)
            };
        }
    }
}
