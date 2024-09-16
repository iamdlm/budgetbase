using BudgetBase.Core.Application.Interfaces.Application;

namespace BudgetBase.Infrastructure.Common.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
