using System.Text;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace BudgetBase.Web.Razor.Areas.Auth.Pages
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public RegisterConfirmationModel(
            IAuthService authService,
            IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public bool IsConfirmed { get; set; }
        
        public string Email { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            StatusMessage = string.Empty;
            Email = email;

            var user = await _userService.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null)
            {
                return Page();
            }

            if (user.EmailConfirmed)
            {
                StatusMessage = "Your email is already confirmed.";
                IsConfirmed = true;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string email)
        {
            Email = email;

            var user = await _userService.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null)
            {
                return Page();
            }

            TokenResponse response = await _authService.GenerateEmailConfirmationAsync(email).ConfigureAwait(false);

            StatusMessage = response.Succeeded ? "Email sent successfully." : "Error sending email. Please try again.";
                        
            return Page();
        }
    }
}
