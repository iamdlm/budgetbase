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
    public class TransactionEditModel : TransactionPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionEntryTypeService _entryTypesService;

        public TransactionEditModel(
            IMapper mapper,
            ITransactionService transactionService,
            ICategoryService categoryService,
            IAccountService accountService,
            ITransactionEntryTypeService entryTypesService,
            IRecurrencyTypeService recurrenciesService)
                : base(accountService, categoryService, entryTypesService, recurrenciesService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
            _entryTypesService = entryTypesService;
        }

        [BindProperty]
        public TransactionViewModel Transaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TransactionDto dto = await _transactionService.GetByIdAsync(id.Value).ConfigureAwait(false);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                Transaction = _mapper.Map<TransactionViewModel>(dto);
            }

            await SetSelectListsAsync().ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            try
            {
                TransactionDto dto = _mapper.Map<TransactionDto>(Transaction);
                dto.Date = new DateTime(dto.Date.Ticks, DateTimeKind.Utc);

                EnumDto entryType = await _entryTypesService.GetByNameAsync(Constants.TransactionEntryTypes.Manual).ConfigureAwait(false);
                dto.TransactionEntryType = entryType;
                dto.TransactionEntryTypeId = entryType.Id;

                await _transactionService.UpdateAsync(dto).ConfigureAwait(false);
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
            await SetAccountIdSelectListAsync(Transaction.SourceAccountId, Transaction.TargetAccountId).ConfigureAwait(false);
            await SetTransactionCategoryIdSelectListAsync(Transaction.TransactionCategoryId).ConfigureAwait(false);
            await SetRecurrencyTypeSelectListAsync(Transaction.RecurrencyTypeId).ConfigureAwait(false);
        }
    }
}
