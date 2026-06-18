using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ToDoManager.Domain.Common;


namespace ToDoManager.Persistence.Interceptors
{
    internal class EntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context;

            if (context is null)
            {
                return result;
            }

            var currentTime = DateTime.UtcNow;

            foreach(var entry in context.ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = currentTime;
                        entry.Entity.UpdatedAt = currentTime;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = currentTime;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.Entity.UpdatedAt = currentTime;
                        entry.State = EntityState.Modified;
                        break;
                }
            }

            return base.SavingChanges(eventData, result);
        }    
    }
}