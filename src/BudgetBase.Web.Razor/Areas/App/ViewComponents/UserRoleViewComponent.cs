using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBase.Web.Razor.Areas.App.ViewComponents
{
    public class UserRoleViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public UserRoleViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string role = await _userService.GetUserRoleAsync(HttpContext.User).ConfigureAwait(false);

            return View("Default", role);
        }
    }
}
