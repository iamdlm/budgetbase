using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Profile;
using Microsoft.AspNetCore.Mvc.Rendering;
using BudgetBase.Core.Application.Interfaces.Persistence;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Profile
{
    public class IndexModel : BreadcrumbPageModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;

        public IndexModel(
            IUserService userService,
            IAuthService authService,
            IMapper mapper,
            ICountryService countryService) : base(userService)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
            _countryService = countryService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ProfileViewModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _authService.GetCurrentUserAsync(User).ConfigureAwait(false);

            if (user == null)
            {
                return NotFound("Unable to load user.");
            }

            Input = _mapper.Map<ProfileViewModel>(user);

            await SetCountryIdSelectListAsync().ConfigureAwait(false);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationUserDto dto = _mapper.Map<ApplicationUserDto>(Input);
            var result = await _userService.UpdateUserAsync(User, dto).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update user.";
                return RedirectToPage();
            }

            await _authService.RefreshSignInAsync(User).ConfigureAwait(false);
            StatusMessage = "Your profile has been updated.";
            return RedirectToPage();
        }

        private async Task SetCountryIdSelectListAsync(object selectedCountryId = null)
        {
            var countries = await _countryService.GetAllAsync().ConfigureAwait(false);
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", selectedCountryId);
        }
    }
}
