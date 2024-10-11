using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InstitutionsService.Data.Interceptors
{
    public class InstitutionReplicationInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            foreach (var entry in eventData.Context.ChangeTracker.Entries<InstitutionReplication>())
            {
                if (entry.State == EntityState.Added && entry.Entity.PlannedPublishDateTime == null && entry.Entity.PublishedDateTime == null)
                {                   
                    entry.Property(x => x.PublishedDateTime).CurrentValue =DateTime.Now;                                  
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
