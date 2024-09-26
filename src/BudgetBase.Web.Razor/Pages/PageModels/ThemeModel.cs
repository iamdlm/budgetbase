using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetBase.Web.Razor.Pages.PageModels
{
    public class ThemeModel : PageModel
    {
        protected readonly IUserService _userService;

        public string ThemeOpt { get; protected set; } = "light";

        protected ThemeModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task InitializeThemeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                ThemeOpt = await _userService.GetThemeOptAsync().ConfigureAwait(false);
            }
        }

        public async Task<IActionResult> OnPostUpdateThemePreferenceAsync(string theme)
        {
            await _userService.UpdateThemeOptAsync(theme).ConfigureAwait(false);
            return RedirectToPage();
        }

        public async Task<JsonResult> OnGetGetThemePreference()
        {
            // Fetch the user's theme preference from the database
            var theme = await _userService.GetThemeOptAsync().ConfigureAwait(false);
            ThemeOpt = theme;

            return new JsonResult(new { theme });
        }
    }
}
