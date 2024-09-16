using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Application;
using FluentValidation;

namespace BudgetBase.Core.Application.Validators.Persistence
{
    public class TransactionDtoValidator : AbstractValidator<TransactionDto>
    {
        public IDateTimeService _dateTimeService { get; }

        public TransactionDtoValidator(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;

            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(t => t.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

            RuleFor(t => t.Date)
                .Must(NotBeInTheFuture)
                .WithMessage("Date cannot be in the future.");
        }

        private bool NotBeInTheFuture(DateTime e)
        {
            try
            {
                return e <= _dateTimeService.Now.Date;
            }
            catch
            {
                // In case of an invalid date
                return false;
            }
        }
    }
}
