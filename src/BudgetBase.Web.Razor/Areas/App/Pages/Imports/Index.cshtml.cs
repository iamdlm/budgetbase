using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Interfaces.Identity;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.Pages.PageModels;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Imports;
using System.Linq.Expressions;

namespace BudgetBase.Web.Razor.Areas.App.Pages.Imports
{
    public class ImportIndexModel : BaseIndexModel<ImportListViewModel, ImportViewModel, ImportDto>
    {
        public ImportIndexModel(IMapper mapper, IImportService importService, IUserService userService)
             : base(mapper, importService, userService)
        {
        }

        public IList<ImportViewModel> Imports { get; set; } = default!;

        protected override Expression<Func<ImportViewModel, object>> GetSortPropertyExpression(string sortField)
        {
            switch (sortField)
            {
                case "Bank":
                    return m => m.Bank.Name;
                case "Source Account":
                    return m => m.SourceAccount.Name;
                case "Created On":
                    return m => m.CreatedOn;
                case "File Name":
                    return m => m.FileName;
                    case "Processed":
                    return m => m.TransactionsCount;
                case "Inserted":
                    return m => m.TransactionsInserted;
                default:
                    return m => m.Bank.Name;
            }
        }

        protected override Expression<Func<ImportViewModel, object>> GetIncludePropertiesExpression()
        {
            return m => new
            {
                Bank = m.Bank.Name,
                SourceAccount = m.SourceAccount.Name
            };
        }

        protected override List<ImportListViewModel> FormatData(IEnumerable<ImportViewModel> data)
        {
            return data.Select(item => new ImportListViewModel
            {
                Id = item.Id,
                BankName = item.Bank.Name,
                SourceAccountName = item.SourceAccount.Name,
                FileName = item.FileName,
                TransactionsCount = item.TransactionsCount,
                TransactionsInserted = item.TransactionsInserted,
                Date = item.CreatedOn.ToString("yyyy-MM-dd"),
                Actions = item.Id
            }).ToList();
        }
    }
}
