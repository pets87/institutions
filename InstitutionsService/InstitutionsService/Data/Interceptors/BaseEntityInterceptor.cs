using InstitutionsService.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InstitutionsService.Data.Interceptors
{
    public class BaseEntityInterceptor : SaveChangesInterceptor 
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public BaseEntityInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = eventData.Context.ChangeTracker.Entries<BaseEntity>();

            string? user = null;
            if (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("UserPersonCode", out var userPersonCode) == true)
            {
                user = userPersonCode.FirstOrDefault();
            }

            user = "38605043778"; //hardcoded for demo purposes

            UpdateBaseEntityFields(entries, user);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateBaseEntityFields(IEnumerable<EntityEntry<BaseEntity>> entries, string? user)
        {
            if (string.IsNullOrWhiteSpace(user))
                return;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedDateTime).CurrentValue = DateTime.UtcNow;
                }
                if (!string.IsNullOrWhiteSpace(user))
                {
                    entry.Property(x => x.UpdatedBy).CurrentValue = user;

                    if (entry.Property(x => x.DeletedDateTime).CurrentValue != null) {
                        entry.Property(x => x.DeletedBy).CurrentValue = user;
                    }
                }
            }
        }
    }
}
