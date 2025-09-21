
using LinkDev.Talabat.Application.Abstraction;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors
{
    class AuditInterCeptor:SaveChangesInterceptor
    {
        private readonly ILoggedInUserService _loggedInUserService;
  
       public AuditInterCeptor(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null)
                return;

            var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
                                    .Where(entity => entity.State is EntityState.Added or EntityState.Modified);

            foreach(var entry in entries)
            {
                if(entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = _loggedInUserService.UserId;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }
                entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
            }
        }
    }
}
