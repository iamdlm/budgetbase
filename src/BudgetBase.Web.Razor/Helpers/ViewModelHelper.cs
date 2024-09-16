using System.Linq.Expressions;

namespace BudgetBase.Web.Razor.Helpers
{
    public static class ViewModelHelper<TViewModel>
    {
        public static void GetVisibleColumns(Expression<Func<TViewModel, object>> includePropertiesExpression, List<string> visibleColumns, List<string> includeProperties)
        {
            if (includePropertiesExpression != null)
            {
                TraverseExpression(includePropertiesExpression.Body, visibleColumns, includeProperties);
            }
        }

        private static void TraverseExpression(Expression expression, List<string> visibleColumns, List<string> includeProperties)
        {
            if (expression is MemberExpression memberExpression)
            {
                TraverseExpression(memberExpression.Expression, visibleColumns, includeProperties);

                if (memberExpression.Expression != null && memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    includeProperties.Insert(0, visibleColumns[0]);

                    // Concatenate nested properties for visibleColumns
                    visibleColumns[0] += "." + memberExpression.Member.Name;
                }
                else
                {
                    visibleColumns.Insert(0, memberExpression.Member.Name);
                }
            }
            else if (expression is NewExpression newExpression)
            {
                foreach (var argument in newExpression.Arguments)
                {
                    TraverseExpression(argument, visibleColumns, includeProperties);
                }
            }
            else if (expression is ConstantExpression constantExpression && constantExpression.Value is string strValue)
            {
                // When the expression is a string, add it directly to includeProperties
                includeProperties.Add(strValue);
            }
        }
    }
}
