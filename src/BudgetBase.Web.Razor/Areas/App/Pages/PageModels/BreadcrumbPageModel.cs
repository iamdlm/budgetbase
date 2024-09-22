using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Breadcrumbs;
using BudgetBase.Web.Razor.Pages.PageModels;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.PageModels
{
    public class BreadcrumbPageModel : ThemeModel
    {
        public List<BreadcrumbViewModel> Breadcrumbs { get; private set; }
        protected BreadcrumbPageModel(IUserService userService) : base(userService)
        {
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var pathSegments = Request.Path.Value.Trim('/').Split('/');
            Breadcrumbs = new List<BreadcrumbViewModel>();

            for (int i = 0; i < pathSegments.Length; i++)
            {
                if (i == 0)
                {
                    Breadcrumbs.Add(new BreadcrumbViewModel
                    {
                        Area = pathSegments[0],
                        Action = "/Dashboard",
                        Title = "Dashboard"
                    });
                }
                else if (i == 1)
                {
                    Breadcrumbs.Add(new BreadcrumbViewModel
                    {
                        Area = pathSegments[0],
                        Action = $"/{pathSegments[1]}/Index",
                        Title = GetRouteTitle(pathSegments[1])
                    });
                }
                else
                {
                    Breadcrumbs.Add(new BreadcrumbViewModel
                    {
                        Area = pathSegments[0],
                        Action = $"/{pathSegments[1]}/{pathSegments[i]}",
                        Title = GetRouteTitle(pathSegments[i])
                    });
                }
            }

            base.OnPageHandlerExecuting(context);
        }

        private Dictionary<string, string> RouteTitles = new()
        {
            { "changepassword", "Change Password" },
            { "rulesgroups", "Categorizations" }
        };
                
        private string GetRouteTitle(string route)
        {
            if (RouteTitles.TryGetValue(route, out string? value))
            {
                return value;
            }

            return route;
        }
    }
}
