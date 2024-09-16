using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.Interfaces.Identity;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetBase.Web.Razor.Areas.Auth.Pages
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public ConfirmEmailModel(
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

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            StatusMessage = string.Empty;

            if (userId == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userService.FindByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            Email = user.Email;

            if (user != null && user.EmailConfirmed)
            {
                StatusMessage = "Your email is already confirmed.";
                IsConfirmed = true;
            }
            else
            {
                var result = await _authService.ConfirmEmailAsync(new EmailConfirmationRequest()
                {
                    UserId = userId,
                    Token = code
                }).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    StatusMessage = "Thank you for confirming your email.";
                    IsConfirmed = true;
                }
                else
                {
                    StatusMessage = "Error confirming your email. Please try again.";
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string email)
        {
            // send email
            TokenResponse response = await _authService.GenerateEmailConfirmationAsync(email).ConfigureAwait(false);

            if (response.Succeeded)
            {
                return RedirectToPage("RegisterConfirmation", new string[] { Email });
            }

            StatusMessage = "Error sending email. Please try again.";
            return Page();
        }
    }
}
