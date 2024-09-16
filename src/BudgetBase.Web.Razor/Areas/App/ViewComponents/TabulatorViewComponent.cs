using Microsoft.AspNetCore.Mvc;

namespace BudgetBase.Web.Razor.Areas.App.ViewComponents
{
    public class TabulatorViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string ajaxUrl, bool allowImport = false, bool editVisible = true, bool applyRules = false)
        {
            ViewData["AjaxUrl"] = ajaxUrl;
            ViewData["AllowImport"] = allowImport;
            ViewData["EditVisible"] = editVisible;
            ViewData["ApplyRules"] = applyRules;
            return Task.FromResult<IViewComponentResult>(View());
        }
    }
}
