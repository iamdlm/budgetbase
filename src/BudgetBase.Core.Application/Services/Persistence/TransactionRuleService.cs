using AutoMapper;
using BudgetBase.Core.Application.DTOs.Application;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Application.Validators.Persistence;
using BudgetBase.Core.Domain.Entities;
using BudgetBase.Core.Domain.Models;
using System.Drawing;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class TransactionRuleService : BaseService<TransactionRule, TransactionRuleDto>, ITransactionRuleService
    {
        public IUnitOfWork _unitOfWork { get; }
        public TransactionRuleService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService) : base(unitOfWork, mapper, currentUserService)
        {
            _unitOfWork = unitOfWork;
        }

        protected override Task ValidateCreateDtoAsync(TransactionRuleDto dto)
        {
            var validator = new TransactionRuleDtoValidator();
            var result = validator.Validate(dto);

            if (result?.IsValid != true)
            {
                throw new ValidationException(result.Errors);
            }

            return Task.CompletedTask;
        }

        protected override async Task ValidateUpdateDtoAsync(TransactionRuleDto dto)
        {
            await ValidateCreateDtoAsync(dto).ConfigureAwait(false);
        }

        public PaginatedResult<TransactionRuleDto> GetByRulesGroupId(int pageNumber, int pageSize, string search, string sortProperty, string sortDirection, Guid groupId)
        {
            IQueryable<TransactionRule> query;

            if (string.IsNullOrWhiteSpace(search))
            {
                // If search is empty or null, fetch all records without applying the search filter
                query = _unitOfWork.TransactionRulesRepo
                    .GetAll(
                        r => r.TransactionRulesGroupId == groupId,
                        null, // Assuming no sorting is specified here
                        null,
                        i => i.TransactionRuleField, i => i.TransactionRuleCondition);
            }
            else
            {
                // If search is not empty, apply the search filter
                query = _unitOfWork.TransactionRulesRepo
                    .GetAll(
                        r => r.TransactionRulesGroupId == groupId &&
                        (r.Name.Contains(search) || r.Value.Contains(search) || r.TransactionRuleField.Description.Contains(search) || r.TransactionRuleCondition.Description.Contains(search)),
                        null, // Assuming no sorting is specified here
                        null,
                        i => i.TransactionRuleField, i => i.TransactionRuleCondition);
            }

            switch (sortProperty)
            {
                case "Name":
                    query = sortDirection == "asc" ? query.OrderBy(r => r.Name) : query.OrderByDescending(r => r.Name);
                    break;
                case "Value":
                    query = sortDirection == "asc" ? query.OrderBy(r => r.Value) : query.OrderByDescending(r => r.Value);
                    break;
                case "TransactionRuleField.Description":
                    query = sortDirection == "asc" ? query.OrderBy(r => r.TransactionRuleField.Description) : query.OrderByDescending(r => r.TransactionRuleField.Description);
                    break;
                case "TransactionRuleCondition.Description":
                    query = sortDirection == "asc" ? query.OrderBy(r => r.TransactionRuleCondition.Description) : query.OrderByDescending(r => r.TransactionRuleCondition.Description);
                    break;
                default:
                    query = query.OrderBy(r => r.Name);
                    break;
            }

            var rules = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PaginatedResult<TransactionRuleDto>()
            {
                LastPage = (int)Math.Ceiling((double)rules.Count() / pageSize),
                LastRow = rules.Count(),
                Items = _mapper.Map<IEnumerable<TransactionRuleDto>>(rules)
            };
        }
    }
}
