using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Identity;
using System.Security.Claims;

namespace BudgetBase.Core.Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<ApplicationUserDto> FindByIdAsync(string userId);
        Task<ApplicationUserDto> FindByEmailAsync(string email);
        Task<string> GetUserIdAsync(ClaimsPrincipal principal);
        Task<string> GetUserRoleAsync(ClaimsPrincipal principal);
        Task<string> GetEmailAsync(ClaimsPrincipal principal);
        Task<string> GetUserNameAsync(ClaimsPrincipal principal);
        Task<string> GetPhoneNumberAsync(ClaimsPrincipal principal);
        Task<ApplicationResponse> ChangeEmailAsync(ClaimsPrincipal principal, string email, string code);
        Task<ApplicationResponse> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword);
        Task<bool> IsEmailConfirmedAsync(string email);
        Task<bool> HasPasswordAsync(ClaimsPrincipal principal);
        Task<ApplicationResponse> SetPhoneNumberAsync(ClaimsPrincipal principal, string phoneNumber);
        Task<ApplicationResponse> AddPasswordAsync(ClaimsPrincipal principal, string newPassword);
        Task<ApplicationResponse> UpdateUserAsync(ClaimsPrincipal principal, ApplicationUserDto user);
        Task<string> GetThemeOptAsync();
        Task UpdateThemeOptAsync(string theme);
    }
}
