using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Application.Specifications;
using BudgetBase.Core.Application.Validators.Persistence;
using BudgetBase.Core.Domain.Entities;
using BudgetBase.Core.Domain.Interfaces;
using BudgetBase.Core.Domain.Models;

namespace BudgetBase.Core.Application.Services.Persistence
{
    public class TransactionRulesGroupService : BaseService<TransactionRulesGroup, TransactionRulesGroupDto>, ITransactionRulesGroupService
    {
        public TransactionRulesGroupService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService) : base(unitOfWork, mapper, currentUserService)
        { }

        public async Task ApplyRulesAsync()
        {
            ISpecification<Transaction> transactionSpec = new OwnedByUserSpecification<Transaction>(_currentUserService.UserId);
            ISpecification<TransactionRulesGroup> rulesGroupSpec = new OwnedByUserSpecification<TransactionRulesGroup>(_currentUserService.UserId);

            IEnumerable<Transaction> transactions = _unitOfWork.TransactionRepo.GetWhere(
                t => t.TransactionCategoryId == null || t.TransactionCategoryId == Guid.Empty,
                transactionSpec).ToList();

            IEnumerable<TransactionRulesGroup> groups = _unitOfWork.TransactionRulesGroupsRepo.GetAll(
                null,
                null,
                rulesGroupSpec,
                i => i.TransactionRules,
                i => i.TransactionCategory,
                i => i.TransactionRulesGroupOperator).ToList();

            foreach (var transaction in transactions)
            {
                await ApplyRulesAsync(transaction, groups).ConfigureAwait(false);
            }

            await _unitOfWork.CompleteAsync().ConfigureAwait(false);
        }

        public async Task ApplyRulesAsync(Transaction transaction, IEnumerable<TransactionRulesGroup> groups)
        {
            foreach (var group in groups)
            {
                bool groupMatch = false; // Used to determine if the group matches based on the operator
                bool allRulesMatch = true; // Assume all rules match initially for And operator

                foreach (var rule in group.TransactionRules)
                {
                    bool isMatch = false;

                    TransactionRuleField ruleField = await _unitOfWork.TransactionRuleFieldsRepo.GetByIdAsync(rule.TransactionRuleFieldId).ConfigureAwait(false);
                    TransactionRuleCondition ruleCondition = await _unitOfWork.TransactionRuleConditionsRepo.GetByIdAsync(rule.TransactionRuleConditionId).ConfigureAwait(false);

                    if (ruleField != null)
                    {
                        if (ruleField.Description.Equals(Constants.TransactionRuleFields.Description))
                        {
                            isMatch = EvaluateCondition(transaction.Description, rule.Value, ruleCondition.Description);
                        }
                        else if (ruleField.Description.Equals(Constants.TransactionRuleFields.Date))
                        {
                            if (DateTime.TryParse(rule.Value, out DateTime ruleDate))
                            {
                                isMatch = EvaluateCondition(transaction.Date, ruleDate, ruleCondition.Description);
                            }
                        }
                        else if (ruleField.Description.Equals(Constants.TransactionRuleFields.Amount))
                        {
                            if (decimal.TryParse(rule.Value, out decimal ruleAmount))
                            {
                                isMatch = EvaluateCondition(transaction.Amount, ruleAmount, ruleCondition.Description);
                            }
                        }
                    }

                    // Adjust logic based on the group operator
                    if (group.TransactionRulesGroupOperator.Description == Constants.TransactionRulesGroupOperators.And)
                    {
                        if (!isMatch)
                        {
                            allRulesMatch = false;
                            break; // If any rule does not match in And operator, break out of the inner loop
                        }
                    }
                    else if (group.TransactionRulesGroupOperator.Description == Constants.TransactionRulesGroupOperators.Or)
                    {
                        if (isMatch)
                        {
                            groupMatch = true;
                            break; // If any rule matches in Or operator, set groupMatch to true and break out of the loop
                        }
                    }
                }

                // Determine if the group matches based on the operator and process accordingly
                if ((group.TransactionRulesGroupOperator.Description == Constants.TransactionRulesGroupOperators.And && allRulesMatch) ||
                    (group.TransactionRulesGroupOperator.Description == Constants.TransactionRulesGroupOperators.Or && groupMatch))
                {
                    TransactionCategory category = await _unitOfWork.TransactionCategoryRepo.GetByIdAsync(group.TransactionCategoryId).ConfigureAwait(false);
                    if (category != null)
                    {
                        transaction.TransactionCategory = category;
                        transaction.TransactionCategoryId = group.TransactionCategoryId;
                        break; // Stop checking other groups once a matching group is found
                    }
                }
            }
        }

        protected override Task ValidateCreateDtoAsync(TransactionRulesGroupDto dto)
        {
            var validator = new TransactionRulesGroupDtoValidator();
            var result = validator.Validate(dto);

            if (result?.IsValid != true)
            {
                throw new ValidationException(result.Errors, "Transaction");
            }

            return Task.CompletedTask;
        }

        protected override async Task ValidateUpdateDtoAsync(TransactionRulesGroupDto dto)
        {
           await ValidateCreateDtoAsync(dto).ConfigureAwait(false);
        }

        private static bool EvaluateCondition<T>(T transactionValue, T ruleValue, string condition) where T : IComparable<T>
        {
            if (condition.Equals(Constants.TransactionRuleConditions.Equals))
            {
                return transactionValue.Equals(ruleValue);
            }
            else if (condition.Equals(Constants.TransactionRuleConditions.NotEquals))
            {
                return !transactionValue.Equals(ruleValue);
            }
            else if (condition.Equals(Constants.TransactionRuleConditions.GreaterThan))
            {
                return transactionValue.CompareTo(ruleValue) > 0;
            }
            else if (condition.Equals(Constants.TransactionRuleConditions.GreaterThanOrEquals))
            {
                return transactionValue.CompareTo(ruleValue) >= 0;
            }
            else if (condition.Equals(Constants.TransactionRuleConditions.LessThan))
            {
                return transactionValue.CompareTo(ruleValue) < 0;
            }
            else if (condition.Equals(Constants.TransactionRuleConditions.LessThanOrEquals))
            {
                return transactionValue.CompareTo(ruleValue) <= 0;
            }
            else if (condition.Equals(Constants.TransactionRuleConditions.Contains))
            {
                if (transactionValue is string strTransaction && ruleValue is string strRule)
                {
                    return strTransaction.Contains(strRule, StringComparison.InvariantCultureIgnoreCase);
                }
            }

            return false;
        }
    }
}
