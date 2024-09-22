using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Imports;
using BudgetBase.Web.Razor.Helpers;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Imports
{
    public class ImportDeleteModel : ImportPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly IImportService _ImportService;

        public ImportDeleteModel(
            IMapper mapper,
            IImportService ImportService,
            ICountryService countryService,
            IBankService bankService,
            IAccountService accountService,
            IUserService userService)
            : base(countryService, bankService, accountService, userService)
        {
            _mapper = mapper;
            _ImportService = ImportService;
        }

        [BindProperty]
        public ImportViewModel Import { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ImportDto dto = await _ImportService.GetByIdAsync(id.Value).ConfigureAwait(false);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                Import = _mapper.Map<ImportViewModel>(dto);
            }
            Guid countryId = _bankService.GetByIdAsync(dto.BankId).Result.CountryId;
            await SetCountryIdSelectListAsync(countryId).ConfigureAwait(false);
            await SetBankIdSelectListAsync(countryId, dto.BankId).ConfigureAwait(false);
            await SetAccountIdSelectListAsync(dto.SourceAccountId).ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _ImportService.DeleteImportAsync(id.Value).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
