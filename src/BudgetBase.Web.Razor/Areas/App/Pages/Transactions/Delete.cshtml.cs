using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Transactions;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Transactions
{
    public class TransactionDeleteModel : TransactionPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public TransactionDeleteModel(
            IMapper mapper,
            ITransactionService transactionService,
            ICategoryService categoryService,
            IAccountService accountService,
            ITransactionEntryTypeService entryTypesService,
            IRecurrencyTypeService recurrenciesService,
            IUserService userService) : base(accountService, categoryService, entryTypesService, recurrenciesService, userService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _transactionService.DeleteAsync(id.Value).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
            }

            return RedirectToPage("./Index");
        }

        private async Task SetSelectListsAsync()
        {
            await SetAccountIdSelectListAsync(Transaction?.SourceAccountId, Transaction?.TargetAccountId).ConfigureAwait(false);
            await SetTransactionCategoryIdSelectListAsync(Transaction?.TransactionCategoryId).ConfigureAwait(false);
            await SetTransactionEntryTypeSelectListAsync(Transaction?.TransactionEntryTypeId).ConfigureAwait(false);
            await SetRecurrencyTypeSelectListAsync(Transaction?.RecurrencyTypeId).ConfigureAwait(false);
        }
    }
}
