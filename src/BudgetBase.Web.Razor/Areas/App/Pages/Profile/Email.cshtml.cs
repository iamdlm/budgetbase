using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Profile
{
    public class EmailModel : BreadcrumbPageModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        //private readonly IEmailSender _emailSender;

        public EmailModel(
            IUserService userService, IAuthService authService) : base(userService)
        {
            _userService = userService;
            _authService = authService;
            //_emailSender = emailSender;
        }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync()
        {
            var user = await _authService.GetCurrentUserAsync(User).ConfigureAwait(false);
            Email = user.Email;

            Input = new InputModel
            {
                NewEmail = user.Email,
            };

            IsEmailConfirmed = user.EmailConfirmed;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _authService.GetCurrentUserAsync(User).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound($"Unable to load user.");
            }

            await LoadAsync().ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _authService.GetCurrentUserAsync(User).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound($"Unable to load user.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync().ConfigureAwait(false);
                return Page();
            }

            if (Input.NewEmail != user.Email)
            {
                TokenResponse response = await _authService.GenerateEmailChangeAsync(User, Input.NewEmail).ConfigureAwait(false);

                StatusMessage = response.Succeeded ? "Confirmation link to change email sent. Please check your email." : "Error sending confirmation link. Please try again.";

                return RedirectToPage();
            }

            StatusMessage = "Your email is unchanged.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadAsync().ConfigureAwait(false);
                return Page();
            }
            
            TokenResponse confirmationResponse = await _authService.GenerateEmailConfirmationAsync(User).ConfigureAwait(false);
            
            var callbackUrl = Url.Page(
                "ConfirmEmail",
                pageHandler: null,
                values: new { area = "Auth", confirmationResponse.Token },
                protocol: Request.Scheme);
            
            //await _emailSender.SendEmailAsync(
            //    email,
            //    "Confirm your email",
            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
