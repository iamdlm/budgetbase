using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetBase.Web.Razor.Areas.Auth.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(
            IAuthService authService, 
            ILogger<LogoutModel> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            await _authService.SignOutAsync().ConfigureAwait(false);
        }
    }
}
