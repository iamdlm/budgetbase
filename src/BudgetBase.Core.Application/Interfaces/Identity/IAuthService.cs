using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Identity;
using System.Security.Claims;

namespace BudgetBase.Core.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<bool> SignInAsync(SignInRequest signInRequest);
        Task SignOutAsync();
        Task<ApplicationResponse> SignUpAsync(SignUpRequest signUpRequest);
        Task<ApplicationResponse> ChangePasswordAsync(ClaimsPrincipal user, string currentPassword, string newPassword);
        Task<ApplicationResponse> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
        Task<TokenResponse> GeneratePasswordResetTokenAsync(string email);
        Task<ApplicationUserDto> GetCurrentUserAsync(ClaimsPrincipal user);
        Task<TokenResponse> GenerateEmailConfirmationAsync(ClaimsPrincipal user);
        Task<TokenResponse> GenerateEmailConfirmationAsync(string email);
        Task<TokenResponse> GenerateEmailChangeAsync(ClaimsPrincipal user, string newEmail);
        Task<ApplicationResponse> ConfirmEmailAsync(EmailConfirmationRequest emailConfirmationRequest);
        Task RefreshSignInAsync(ClaimsPrincipal user);        
    }
}
