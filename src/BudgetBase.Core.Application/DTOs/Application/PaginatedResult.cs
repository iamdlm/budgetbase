namespace BudgetBase.Core.Application.DTOs.Application
{
    public class PaginatedResult<T> where T : class
    {
        public int LastPage { get; set; }
        public int LastRow { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
