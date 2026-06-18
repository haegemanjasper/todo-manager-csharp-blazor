using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Domain.ToDoItems;

namespace ToDoManager.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<ToDoItem>().HasQueryFilter(s => !s.IsDeleted);
        }
    }
}