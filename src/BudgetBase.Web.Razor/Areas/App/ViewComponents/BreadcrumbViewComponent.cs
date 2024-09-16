using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Breadcrumbs;

namespace BudgetBase.Web.Razor.Areas.App.ViewComponents
{
    public class BreadcrumbViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<BreadcrumbViewModel> breadcrumbModels)
        {
            return View(breadcrumbModels);
        }
    }
}
