using BudgetBase.Core.Application.DTOs.Persistence;
using FluentValidation;

namespace BudgetBase.Core.Application.Validators.Persistence
{
    public class ImportDtoValidator : AbstractValidator<ImportDto>
    {
        public ImportDtoValidator()
        {
        }
    }
}
