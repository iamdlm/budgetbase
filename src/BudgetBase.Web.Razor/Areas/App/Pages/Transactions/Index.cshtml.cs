using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Core.Application.Services.Persistence;
using BudgetBase.Core.Domain.Entities;
using BudgetBase.Core.Domain.Models;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Web;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Transactions
{
    public class TransactionIndexModel : BaseIndexModel<TransactionListViewModel, TransactionViewModel, TransactionDto>
    {
        public ITransactionRulesGroupService _rulesService { get; }

        public TransactionIndexModel(
            IMapper mapper,
            ITransactionService transactionService,
            ITransactionRulesGroupService rulesService,
            IUserService userService)
            : base(mapper, transactionService, userService)
        {
            _rulesService = rulesService;
        }

        public IList<TransactionViewModel> Transactions { get; set; } = default!;

        public async Task<JsonResult> OnPostApplyRulesAsync()
        {
            try
            {
                await _rulesService.ApplyRulesAsync().ConfigureAwait(false);

                return new JsonResult(true);
            }
            catch (Exception)
            {
                return new JsonResult(false);
            }
        }

        protected override Expression<Func<TransactionViewModel, object>> GetSortPropertyExpression(string sortField)
        {
            switch (sortField)
            {
                case "Description":
                    return m => m.Description;
                case "Amount":
                    return m => m.Amount;
                case "Date":
                    return m => m.Date;
                case "Source Account":
                    return m => m.SourceAccount.Name;
                case "Target Account":
                    return m => m.TargetAccount.Name;
                case "Category":
                    return m => m.TransactionCategory.Title;
                case "Entry":
                    return m => m.TransactionEntryType.Description;
                default:
                    return m => m.Date;
            }
        }

        protected override Expression<Func<TransactionViewModel, object>> GetIncludePropertiesExpression()
        {
            return m => new
            {
                m.Description,
                m.Amount,
                m.Date,
                SourceAccount = m.SourceAccount.Name,
                TargetAccount = m.TargetAccount.Name,
                TransactionCategory = m.TransactionCategory.Title,
                TransactionEntryType = m.TransactionEntryType.Description
            };
        }

        protected override List<TransactionListViewModel> FormatData(IEnumerable<TransactionViewModel> data)
        {
            return data.Select(item => new TransactionListViewModel
            {
                Id = item.Id,
                SourceAccountName = item.SourceAccount.Name,
                TargetAccountName = item.TargetAccount?.Name,
                Amount = string.Format("{0:.00}", item.Amount),
                Date = item.Date.ToString("yyyy-MM-dd"),
                Description = item.Description,
                CategoryTitle = GetCategoryBadge(item.TransactionCategory),
                EntryType = GetEntryTypeBadge(item.TransactionEntryType?.Description),
                Actions = item.Id
            }).ToList();
        }

        private static string GetEntryTypeBadge(string description)
        {
            if (description == Constants.TransactionEntryTypes.Auto)
            {
                return $"<span class=\"badge bg-soft-primary\">{description}</span>";
            }

            if (description == Constants.TransactionEntryTypes.Manual)
            {
                return $"<span class=\"badge bg-soft-success\">{description}</span>";
            }

            return $"<span class=\"badge bg-soft-secondary\">{description}</span>";
        }

        private static string GetCategoryBadge(CategoryDto category)
        {
            if (category == null)
            {
                return string.Empty;
            }

            if  (string.IsNullOrEmpty(category.Color))
            {
                return category.Title;
            }

            return $"<div class=\"d-flex align-items-center\"><span class=\"btn btn-icon fs-5\" style=\"background-color:{category.Color}; color: #fff; cursor: default\">{category.Icon}</span><span class=\"ms-2\">{category.Title}</span></div>";
        }
    }
}
