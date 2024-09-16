using AutoMapper;
using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Imports;
using BudgetBase.Web.Razor.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Imports
{
    public class ImportCreateModel : ImportPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly IImportService _ImportService;
        private readonly IBankTransactionParserService _parserService;

        public ImportCreateModel(
            IMapper mapper,
            IImportService ImportService,
            ICountryService countryService,
            IBankService bankService,
            IAccountService accountService,
            IBankTransactionParserService parserService)
            : base(countryService, bankService, accountService)
        {
            _mapper = mapper;
            _ImportService = ImportService;
            _parserService = parserService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await SetCountryIdSelectListAsync().ConfigureAwait(false);
            await SetAccountIdSelectListAsync().ConfigureAwait(false);
            return Page();
        }

        [BindProperty]
        public ImportViewModel Import { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Import == null)
            {
                await SetCountryIdSelectListAsync().ConfigureAwait(false);
                await SetAccountIdSelectListAsync().ConfigureAwait(false);
                return Page();
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await Import.File.CopyToAsync(stream).ConfigureAwait(false);

                    // Parse transactions
                    string bankIdentifier = _bankService.GetByIdAsync((Guid)Import.BankId).Result.Name.Replace(" ", "");
                    IBankTransactionParser parser = _parserService.GetParser(bankIdentifier);
                    ParserResult result = parser.ParseTransactions(stream);

                    // Save import and transactions
                    ImportDto dto = _mapper.Map<ImportDto>(Import);
                    dto.Filename = Import.File.FileName;
                    dto.TransactionsCount = result.ProcessedCount;
                    dto.TransactionsInserted = result.Transactions.Count;
                    await _ImportService.ImportAsync(dto, result.Transactions, Import.SourceAccountId).ConfigureAwait(false);
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);
                await SetCountryIdSelectListAsync().ConfigureAwait(false);
                await SetAccountIdSelectListAsync().ConfigureAwait(false);
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public Task<JsonResult> OnPostFillBanksSelectListAsync(Guid countryId)
        {
            var banks = _bankService.GetByCountryId(countryId);

            return Task.FromResult(new JsonResult(banks.Select(x => new { value = x.Id, text = x.Name })));
        }
    }
}
