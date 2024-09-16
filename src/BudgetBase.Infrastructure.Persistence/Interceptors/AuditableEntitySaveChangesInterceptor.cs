using BudgetBase.Core.Application.Interfaces.Application;
using BudgetBase.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BudgetBase.Infrastructure.Persistence.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTime;

        public AuditableEntitySaveChangesInterceptor(
            ICurrentUserService currentUserService,
            IDateTimeService dateTime)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            DateTime now = _dateTime.Now;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.CreatedOn = now;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedBy = _currentUserService.UserId;
                    entry.Entity.ModifiedOn = now;
                }
            }
        }
    }
}
