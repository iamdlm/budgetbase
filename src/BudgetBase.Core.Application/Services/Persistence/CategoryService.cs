using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Application.Validators.Persistence;
using BudgetBase.Core.Domain.Entities;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class CategoryService : BaseService<TransactionCategory, CategoryDto>, ICategoryService
    {
        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService) : base(unitOfWork, mapper, currentUserService)
        {
        }

        public IEnumerable<CategoryDto> GetByType(Guid typeId)
        {
            IEnumerable<TransactionCategory> categories = _unitOfWork.TransactionCategoryRepo.GetWhere(c => c.TransactionTypeId == typeId);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public override Task DeleteAsync(Guid id)
        {
            IEnumerable<TransactionCategory> categories = _unitOfWork.TransactionCategoryRepo.GetWhere(c => c.ParentTransactionCategoryId == id);

            if (categories.Any())
            {
                throw new ValidationException("The category already has subcategories and cannot be deleted.");
            }

            return base.DeleteAsync(id);
        }

        protected override async Task ValidateCreateDtoAsync(CategoryDto dto)
        {
            var validator = new CategoryDtoValidator();
            var result = validator.Validate(dto);

            if (result?.IsValid != true)
            {
                throw new ValidationException(result.Errors);
            }

            TransactionCategory existingCat = await _unitOfWork.TransactionCategoryRepo.FirstOrDefaultAsync(
                c => c.Title == dto.Title && c.TransactionTypeId == dto.TransactionTypeId && c.ParentTransactionCategoryId == dto.ParentTransactionCategoryId,
                null,
                null).ConfigureAwait(false);

            if (existingCat != null)
            {
                throw new ValidationException("Category.Title", "A category with the specified title for the selected type already exists.");
            }
        }

        protected override Task ValidateUpdateDtoAsync(CategoryDto dto)
        {
            var validator = new CategoryDtoValidator();
            var result = validator.Validate(dto);

            if (result?.IsValid != true)
            {
                throw new ValidationException(result.Errors);
            }

            return Task.CompletedTask;
        }
    }
}
