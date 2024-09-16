using System.ComponentModel.DataAnnotations;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetBase.Web.Razor.Areas.Auth.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserService _userService;

        public LoginModel(IAuthService authService, ILogger<LoginModel> logger, IUserService userService)
        {
            _authService = authService;
            _logger = logger;
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            ReturnUrl = returnUrl;
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/app/dashboard");

            if (ModelState.IsValid)
            {
                var succeeded = await _authService.SignInAsync(new SignInRequest()
                {
                    Email = Input.Email,
                    Password = Input.Password,
                    RememberMe = Input.RememberMe
                }).ConfigureAwait(false);

                if (succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
