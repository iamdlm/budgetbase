using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Application.Validators.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class AccountService : BaseService<Account, AccountDto>, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mapper, currentUserService)
        {
        }

        public override Task DeleteAsync(Guid id)
        {
            IEnumerable<Transaction> transactions = _unitOfWork.TransactionRepo.GetWhere(c => c.SourceAccountId == id || c.TargetAccountId == id);

            if (transactions.Any())
            {
                throw new ValidationException("The account has at least one transaction associated and cannot be deleted.");
            }

            return base.DeleteAsync(id);
        }

        protected override async Task ValidateCreateDtoAsync(AccountDto dto)
        {
            var validator = new AccountDtoValidator();
            var result = validator.Validate(dto);

            if (result?.IsValid != true)
            {
                throw new ValidationException(result.Errors);
            }

            Account existingAccount = await _unitOfWork.AccountRepo.FirstOrDefaultAsync(c => c.IBAN == dto.IBAN, null, null).ConfigureAwait(false);

            if (existingAccount != null)
            {
                throw new ValidationException("Account.IBAN", "An account with the specified IBAN already exists.");
            }
        }

        protected override Task ValidateUpdateDtoAsync(AccountDto dto)
        {
            var validator = new AccountDtoValidator();
            var result = validator.Validate(dto);

            if (result?.IsValid != true)
            {
                throw new ValidationException(result.Errors);
            }

            return Task.CompletedTask;
        }
    }
}
