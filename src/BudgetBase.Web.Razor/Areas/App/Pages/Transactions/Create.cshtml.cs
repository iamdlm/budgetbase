using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Transactions;
using BudgetBase.Core.Domain.Models;
using BudgetBase.Web.Razor.Helpers;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Transactions
{
    public class TransactionCreateModel : TransactionPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionEntryTypeService _entryTypesService;

        public TransactionCreateModel(
            IMapper mapper,
            IAccountService accountService,
            ICategoryService categoryService,
            ITransactionService transactionService,
            ITransactionEntryTypeService entryTypesService,
            IRecurrencyTypeService recurrenciesService) : base(accountService, categoryService, entryTypesService, recurrenciesService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
            _entryTypesService = entryTypesService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await SetSelectListsAsync().ConfigureAwait(false);
            return Page();
        }

        [BindProperty]
        public TransactionViewModel Transaction { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Transaction == null)
            {
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            try
            {
                TransactionDto dto = _mapper.Map<TransactionDto>(Transaction);
                dto.Date = new DateTime(dto.Date.Ticks, DateTimeKind.Utc);
                dto.TransactionEntryTypeId = _entryTypesService.GetByNameAsync(Constants.TransactionEntryTypes.Manual).Result.Id;
                await _transactionService.CreateAsync(dto).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private async Task SetSelectListsAsync()
        {
            await SetAccountIdSelectListAsync(Transaction?.SourceAccountId, Transaction?.TargetAccountId).ConfigureAwait(false);
            await SetTransactionCategoryIdSelectListAsync(Transaction?.TransactionCategoryId).ConfigureAwait(false);
            await SetRecurrencyTypeSelectListAsync(Transaction?.RecurrencyTypeId).ConfigureAwait(false);
        }
    }
}
