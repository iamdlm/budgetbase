using BudgetBase.Core.Application.Interfaces.Identity;
using AutoMapper;
using BudgetBase.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using BudgetBase.Infrastructure.Identity.Helpers;
using System.Security.Claims;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.DTOs.Application;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using BudgetBase.Core.Application.Interfaces.Application;

namespace BudgetBase.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly string RoleCacheKey = "UserRole_{0}";
        private readonly int RoleCacheDurationMinutes = 30;

        public UserManager<ApplicationUser> _userManager { get; }
        public IMapper _mapper { get; }
        public IMemoryCache _cache { get; }
        private ICurrentUserService _currentUserService { get; }

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, IMemoryCache cache, ICurrentUserService userService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _cache = cache;
            _currentUserService = userService;
        }

        public async Task<ApplicationUserDto> FindByIdAsync(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<ApplicationUserDto> FindByEmailAsync(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<string> GetUserIdAsync(ClaimsPrincipal principal)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
            return await _userManager.GetUserIdAsync(user).ConfigureAwait(false);
        }

        public async Task<string> GetUserRoleAsync(ClaimsPrincipal principal)
        {
            string userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!_cache.TryGetValue(RoleCacheKey, out string role))
            {
                var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
                var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                role = roles.FirstOrDefault(); // Assuming the user has one role

                // Set cache options; e.g., expire in 30 minutes.
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(RoleCacheDurationMinutes));

                _cache.Set(RoleCacheKey, role, cacheEntryOptions);
            }

            return role;
        }

        public async Task<ApplicationResponse> ChangeEmailAsync(ClaimsPrincipal principal, string email, string code)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);

            string token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            IdentityResult result = await _userManager.ChangeEmailAsync(user, email, token).ConfigureAwait(false);

            return result.ToAuthenticationResult();
        }

        public async Task<bool> IsEmailConfirmedAsync(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null)
            {
                return false;
            }

            return await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false);
        }

        public async Task<ApplicationResponse> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
            IdentityResult result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword).ConfigureAwait(false);
            return result.ToAuthenticationResult();
        }

        public async Task<bool> HasPasswordAsync(ClaimsPrincipal principal)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
            return await _userManager.HasPasswordAsync(user).ConfigureAwait(false);
        }

        public async Task<string> GetEmailAsync(ClaimsPrincipal principal)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
            return await _userManager.GetEmailAsync(user).ConfigureAwait(false);
        }

        public async Task<string> GetUserNameAsync(ClaimsPrincipal principal)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
            return await _userManager.GetUserNameAsync(user).ConfigureAwait(false);
        }

        public async Task<string> GetPhoneNumberAsync(ClaimsPrincipal principal)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
            return await _userManager.GetPhoneNumberAsync(user).ConfigureAwait(false);
        }

        public async Task<ApplicationResponse> SetPhoneNumberAsync(ClaimsPrincipal principal, string phoneNumber)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
            IdentityResult result = await _userManager.SetPhoneNumberAsync(user, phoneNumber).ConfigureAwait(false);
            return result.ToAuthenticationResult();
        }

        public async Task<ApplicationResponse> AddPasswordAsync(ClaimsPrincipal principal, string newPassword)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);
            IdentityResult result = await _userManager.AddPasswordAsync(user, newPassword).ConfigureAwait(false);
            return result.ToAuthenticationResult();
        }

        public async Task<ApplicationResponse> UpdateUserAsync(ClaimsPrincipal principal, ApplicationUserDto dto)
        {
            ApplicationUser user = await _userManager.GetUserAsync(principal).ConfigureAwait(false);

            user.FullName = dto.FullName;
            user.CountryId = dto.CountryId;

            IdentityResult result = await _userManager.UpdateAsync(user).ConfigureAwait(false);

            if (result.Succeeded && user.UserName != dto.UserName)
            {
                await _userManager.SetUserNameAsync(user, dto.UserName).ConfigureAwait(false);
            }

            return result.ToAuthenticationResult();
        }

        public async Task UpdateThemeOptAsync(string theme)
        {
            // Get the current user
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId).ConfigureAwait(false);

            if (user != null) {
                // Update the user's theme
                user.ThemeOpt = theme;

                // Update the user
                await _userManager.UpdateAsync(user).ConfigureAwait(false);
            }
        }

        public async Task<string> GetThemeOptAsync()
        {
            // Get the current user
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId).ConfigureAwait(false);

            if (user != null) {
                return user.ThemeOpt;
            }

            return string.Empty;
        }
    }
}
