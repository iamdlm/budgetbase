using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Application.Validators.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class TransactionService : BaseService<Transaction, TransactionDto>, ITransactionService
    {
        IDateTimeService _dateTimeService { get; }

        public TransactionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService
            dateTimeService,
            ICurrentUserService currentUserService) : base(unitOfWork, mapper, currentUserService)
        {
            _dateTimeService = dateTimeService;
        }

        public DateTime GetFirstTransaction()
        {
            IQueryable<Transaction> transactions = _unitOfWork.TransactionRepo.GetWhere(t => t.CreatedBy == _currentUserService.UserId).OrderBy(o => o.CreatedOn);

            if (transactions == null || !transactions.Any())
            {
                return _dateTimeService.Now;
            }

            return transactions.FirstOrDefault().CreatedOn;
        }

        protected override Task ValidateCreateDtoAsync(TransactionDto dto)
        {
            var validator = new TransactionDtoValidator(_dateTimeService);
            var result = validator.Validate(dto);

            if (result?.IsValid != true)
            {
                throw new ValidationException(result.Errors, "Transaction");
            }

            return Task.CompletedTask;
        }

        protected override async Task ValidateUpdateDtoAsync(TransactionDto dto)
        {
            await ValidateCreateDtoAsync(dto).ConfigureAwait(false);
        }
    }
}
