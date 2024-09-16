using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Transactions;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Transactions
{
    public class TransactionDetailsModel : TransactionPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public TransactionDetailsModel(
            IMapper mapper,
            ITransactionService transactionService,
            ICategoryService categoryService,
            IAccountService accountService,
            ITransactionEntryTypeService entryTypesService,
            IRecurrencyTypeService recurrenciesService) : base(accountService, categoryService, entryTypesService, recurrenciesService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
        }

        public TransactionViewModel Transaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                TransactionDto dto = await _transactionService.GetByIdAsync(id.Value).ConfigureAwait(false);

                if (dto == null)
                {
                    return NotFound();
                }
                else
                {
                    Transaction = _mapper.Map<TransactionViewModel>(dto);
                }
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }

            await SetSelectListsAsync().ConfigureAwait(false);

            return Page();
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
