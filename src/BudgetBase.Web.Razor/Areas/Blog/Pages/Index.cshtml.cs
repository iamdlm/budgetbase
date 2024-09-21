using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Web.Razor.Pages.PageModels;

namespace BudgetBase.Web.Razor.Areas.Blog.Pages
{
    public class IndexModel : ThemeModel
    {
        public IndexModel(IUserService userService) : base(userService)
        {
        }

        public void OnGet()
        {
        }
    }
}
