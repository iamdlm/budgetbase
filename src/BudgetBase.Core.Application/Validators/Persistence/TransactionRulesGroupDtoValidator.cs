using BudgetBase.Core.Application.DTOs.Persistence;
using FluentValidation;

namespace BudgetBase.Core.Application.Validators.Persistence
{
    public class TransactionRulesGroupDtoValidator : AbstractValidator<TransactionRulesGroupDto>
    {
        public TransactionRulesGroupDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(t => t.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
        }
    }
}
