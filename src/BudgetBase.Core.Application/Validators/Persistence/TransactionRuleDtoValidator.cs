using BudgetBase.Core.Application.DTOs.Persistence;
using FluentValidation;

namespace BudgetBase.Core.Application.Validators.Persistence
{
    public class TransactionRuleDtoValidator : AbstractValidator<TransactionRuleDto>
    {
        public TransactionRuleDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(t => t.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

            RuleFor(t => t.Value)
            .NotEmpty()
            .WithMessage("Value is required.");
        }
    }
}
