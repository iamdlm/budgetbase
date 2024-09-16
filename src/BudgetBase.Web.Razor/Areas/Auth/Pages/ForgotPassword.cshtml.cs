using System.ComponentModel.DataAnnotations;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetBase.Web.Razor.Areas.Auth.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public ForgotPasswordModel(
            IUserService userService,
            IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!await _userService.IsEmailConfirmedAsync(Input.Email).ConfigureAwait(false))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                TokenResponse response = await _authService.GeneratePasswordResetTokenAsync(Input.Email).ConfigureAwait(false);
                
                if (response.Succeeded)
                {
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }                
            }

            return Page();
        }
    }
}
