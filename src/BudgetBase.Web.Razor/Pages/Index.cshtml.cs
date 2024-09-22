using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Web.Razor.Pages.PageModels;

namespace BudgetBase.Web.Razor.Pages
{
    public class IndexModel : ThemeModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, IUserService userService) : base(userService)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            await InitializeThemeAsync().ConfigureAwait(false);
        }
    }
}