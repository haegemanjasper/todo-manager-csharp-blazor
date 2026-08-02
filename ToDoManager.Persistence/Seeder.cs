using Microsoft.EntityFrameworkCore;
using ToDoManager.Domain.ToDoItems;
using ToDoManager.Shared.Common.Enums;

namespace ToDoManager.Persistence
{
    public class Seeder
    {

        private readonly ApplicationDbContext _dbContext;

        public Seeder(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Seed()
        {
            //ClearAllData();

            var seedingActions = new List<(Func<bool> IsSeeded, Action Seed)>
            {
                (() => _dbContext.ToDoItems.Any(), SeedToDoItems)
            };

            foreach (var (isSeeded, seed) in seedingActions)
            {
                if (!isSeeded())
                {
                    seed();
                    _dbContext.SaveChanges();
                }
            }
        }

        private void SeedToDoItems()
        {

            var items = new List<ToDoItem>
            {
                new()
                {
                    Title = "Preparing for sollicitation 04/08/26",
                    Priority = Priority.High
                },

                new()
                {
                    Title = "Research around SAP and Database Queries",
                    Priority = Priority.Medium
                },
            };

            _dbContext.ToDoItems.AddRange(items);

        }

        private void ClearAllData()
        {
            _dbContext.Database.ExecuteSqlRaw("DELETE FROM [ToDoItems]");
        }
    }
}
