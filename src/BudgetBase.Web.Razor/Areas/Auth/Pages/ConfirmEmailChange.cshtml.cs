using System.Text;
using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace BudgetBase.Web.Razor.Areas.Auth.Pages
{
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public ConfirmEmailChangeModel(
            IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string code)
        {
            if (email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var result = await _userService.ChangeEmailAsync(User, email, code).ConfigureAwait(false);
            
            if (!result.Succeeded)
            {
                StatusMessage = "Error changing email.";
                return Page();
            }

            await _authService.RefreshSignInAsync(User).ConfigureAwait(false);
            StatusMessage = "Thank you for confirming your email change.";
            return Page();
        }
    }
}
