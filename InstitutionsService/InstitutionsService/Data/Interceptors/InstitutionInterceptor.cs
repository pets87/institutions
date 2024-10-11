using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InstitutionsService.Data.Interceptors
{
    public class InstitutionInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var context = eventData.Context;
            foreach (var entry in eventData.Context.ChangeTracker.Entries<Institution>())
            {
                var id = entry.Property(x => x.Id).CurrentValue;
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {                    
                    entry.Property(x => x.NameTranslationCode).CurrentValue = $"{nameof(Institution).ToLower()}.{id}.{nameof(Institution.Name).ToLower()}";
                }
                if (entry.Entity.Address != null && entry.Entity.Address.Id > 0)
                {
                    context.Entry(entry.Entity.Address).State = EntityState.Unchanged;
                }
                if (entry.Entity.Replications?.Any() == true)
                {
                    foreach (var replication in entry.Entity.Replications) 
                    {
                        context.Entry(replication).State = EntityState.Unchanged;
                    }                  
                }
                if (entry.Entity.Translations?.Any() == true)
                {
                    foreach (var translation in entry.Entity.Translations)
                    {
                        if(context.Entry(entry.Entity.Translations[0]).State == EntityState.Added)
                            translation.Code = $"{nameof(Institution).ToLower()}.{id}.{nameof(Institution.Name).ToLower()}";
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
