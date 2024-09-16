using System.ComponentModel.DataAnnotations;
using BudgetBase.Core.Application.DTOs;
using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetBase.Web.Razor.Areas.Auth.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            IAuthService authService,
            ILogger<RegisterModel> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                ApplicationResponse result = await _authService.SignUpAsync(new SignUpRequest()
                {
                    Email = Input.Email,
                    Password = Input.Password
                }).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Value);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
