using System.ComponentModel.DataAnnotations;
using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Profile
{
    public class ChangePasswordModel : BreadcrumbPageModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            IUserService userService, 
            IAuthService authService,
            ILogger<ChangePasswordModel> logger)
        {
            _userService = userService;
            _authService = authService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var hasPassword = await _userService.HasPasswordAsync(User).ConfigureAwait(false);
            
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var changePasswordResult = await _userService.ChangePasswordAsync(User, Input.OldPassword, Input.NewPassword).ConfigureAwait(false);
            
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Value);
                }
                return Page();
            }

            await _authService.RefreshSignInAsync(User).ConfigureAwait(false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToPage();
        }
    }
}
