using AutoMapper;
using BudgetBase.Core.Application.Interfaces.Persistence;
using BudgetBase.Web.Razor.Areas.App.ViewModels.Components.Tabulator;
using BudgetBase.Web.Razor.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BudgetBase.Web.Razor.Areas.App.Pages.PageModels
{
    public abstract class BaseIndexModel<TListViewModel, TViewModel, TDto> : BreadcrumbPageModel
        where TListViewModel : class
        where TViewModel : class
        where TDto : class, IIdentifiable
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseService<TDto> _baseService;

        protected BaseIndexModel(IMapper mapper, IBaseService<TDto> baseService)
        {
            _mapper = mapper;
            _baseService = baseService;
        }

        public virtual async Task<JsonResult> OnGetRemoteDataAsync(int pageNumber, int pageSize, List<Dictionary<string, string>> filters, List<Dictionary<string, string>> sorters, Guid? groupId)
        {
            string search = filters.Any() ? filters[0].FirstOrDefault(f => f.Key == "value").Value ?? string.Empty : string.Empty;
            string sortField = sorters.Any() ? sorters[0].FirstOrDefault(f => f.Key == "field").Value : string.Empty;
            string sortProperty = GetSortProperty(sortField);
            string sortDirection = sorters.Any() ? sorters[0].FirstOrDefault(f => f.Key == "dir").Value : string.Empty;

            List<string> visibleColumns = new();
            List<string> includeProperties = new();

            var includePropertiesExpression = GetIncludePropertiesExpression();

            ViewModelHelper<TViewModel>.GetVisibleColumns(includePropertiesExpression, visibleColumns, includeProperties);

            var response = await _baseService.GetPaginatedAsync(
                pageNumber,
                pageSize,
                search,
                sortProperty,
                sortDirection,
                visibleColumns,
                includeProperties).ConfigureAwait(false);

            var data = _mapper.Map<IEnumerable<TViewModel>>(response.Items);

            var result = new TabulatorListViewModel<TListViewModel>
            {
                LastPage = response.LastPage,
                LastRow = response.LastRow,
                Data = FormatData(data)
            };

            return new JsonResult(result);
        }

        protected abstract Expression<Func<TViewModel, object>> GetIncludePropertiesExpression();
        protected abstract Expression<Func<TViewModel, object>> GetSortPropertyExpression(string sortField);
        protected abstract List<TListViewModel> FormatData(IEnumerable<TViewModel> data);

        public string GetSortProperty(string sortField)
        {
            return GetPropertyNameFromExpression(GetSortPropertyExpression(sortField));
        }

        private string GetPropertyNameFromExpression<TViewModel>(Expression<Func<TViewModel, object>> expression)
        {
            var memberExpressions = new List<string>();
            Expression currentExpression = expression.Body;

            // Handle conversions from a type to object (boxing, e.g., m => (object)m.SomeProperty)
            if (currentExpression.NodeType == ExpressionType.Convert && currentExpression is UnaryExpression unaryExpression)
            {
                currentExpression = unaryExpression.Operand;
            }

            // Traverse the expression tree to extract the full property path
            while (currentExpression is MemberExpression memberExpression)
            {
                memberExpressions.Insert(0, memberExpression.Member.Name);
                currentExpression = memberExpression.Expression;
            }

            return string.Join(".", memberExpressions);
        }
    }
}
