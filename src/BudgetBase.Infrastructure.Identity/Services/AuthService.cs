using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AutoMapper;
using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Infrastructure.Identity.Models;
using BudgetBase.Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.Interfaces.Common;
using DocumentFormat.OpenXml.Office2016.Excel;

namespace BudgetBase.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AuthService(IMapper mapper, UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _mapper = mapper;
            _userManager = UserManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<bool> SignInAsync(SignInRequest request)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);

            if (user == null)
            {
                return false;
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, request.RememberMe, false).ConfigureAwait(false);

            return signInResult.Succeeded;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
        }

        public async Task<ApplicationUserDto> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return null;
            }

            string userId = _userManager.GetUserId(principal);
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            ApplicationUser user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<ApplicationResponse> SignUpAsync(SignUpRequest request)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email.Split('@')[0]
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password).ConfigureAwait(false);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.FreeRole).ConfigureAwait(false);
                await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);

                await GenerateEmailConfirmationAsync(user).ConfigureAwait(false);
            }

            return result.ToAuthenticationResult();
        }

        public async Task<ApplicationResponse> ChangePasswordAsync(ClaimsPrincipal principal, string currentPassword, string newPassword)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);

            if (user == null)
            {
                return new ApplicationResponse()
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string>() { { string.Empty, "Invalid request." } }
                };
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword).ConfigureAwait(false);

            return result.ToAuthenticationResult();
        }

        public async Task<ApplicationResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.UserEmail).ConfigureAwait(false);

            if (user == null)
            {
                return new ApplicationResponse()
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string>() { { string.Empty, "Invalid request." } }
                };
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token)), request.NewPassword).ConfigureAwait(false);

            return result.ToAuthenticationResult();
        }

        public async Task<TokenResponse> GeneratePasswordResetTokenAsync(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null)
            {
                return new TokenResponse()
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string>() { { string.Empty, "Invalid request." } }
                };
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
            await _emailService.SendPasswordResetAsync(user.Email, token).ConfigureAwait(false);

            return new TokenResponse()
            {
                Succeeded = true,
                Token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token))
            };
        }

        public async Task<TokenResponse> GenerateEmailConfirmationAsync(ClaimsPrincipal principal)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);

            return await GenerateEmailConfirmationAsync(user).ConfigureAwait(false);
        }

        public async Task<TokenResponse> GenerateEmailConfirmationAsync(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            return await GenerateEmailConfirmationAsync(user).ConfigureAwait(false);
        }

        private async Task<TokenResponse> GenerateEmailConfirmationAsync(ApplicationUser user)
        {
            if (user == null)
            {
                return new TokenResponse()
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string>() { { string.Empty, "Invalid request." } }
                };
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
            await _emailService.SendEmailConfirmationAsync(user.Email, user.Id, token).ConfigureAwait(false);

            return new TokenResponse()
            {
                Succeeded = true,
                UserId = user.Id,
                Token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token))
            };
        }

        public async Task<ApplicationResponse> ConfirmEmailAsync(EmailConfirmationRequest request)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(request.UserId).ConfigureAwait(false);

            if (user == null)
            {
                return new ApplicationResponse()
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string>() { { string.Empty, "Invalid request." } }
                };
            }

            string token;
            try
            {
                token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            }
            catch (Exception)
            {
                return new ApplicationResponse()
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string>() { { string.Empty, "Invalid token." } }
                };
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token).ConfigureAwait(false);

            if (result.Succeeded)
            {
                var emailConfirmedClaim = new Claim("emailConfirmed", "True");
                await _userManager.AddClaimAsync(user, emailConfirmedClaim).ConfigureAwait(false);
                await _signInManager.RefreshSignInAsync(user).ConfigureAwait(false);
            }

            return result.ToAuthenticationResult();
        }

        public async Task RefreshSignInAsync(ClaimsPrincipal principal)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);

            if (user != null)
            {
                await _signInManager.RefreshSignInAsync(user).ConfigureAwait(false);
            }
        }

        public async Task<TokenResponse> GenerateEmailChangeAsync(ClaimsPrincipal principal, string newEmail)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);

            if (user == null)
            {
                return new TokenResponse()
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string>() { { string.Empty, "Invalid request." } }
                };
            }

            string token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail).ConfigureAwait(false);
            await _emailService.SendEmailConfirmationChangeAsync(newEmail, user.Id, token).ConfigureAwait(false);

            return new TokenResponse()
            {
                Succeeded = true,
                Token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token)),
                UserId = user.Id
            };
        }
    }
}
