using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoManager.Domain.ToDoItems;
using ToDoManager.Persistence.Common;

namespace ToDoManager.Persistence.ToDoItems
{
    internal class ToDoItemConfiguration : EntityConfiguration<ToDoItem>
    {
        public override void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Title).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Priority).HasConversion<string>().IsRequired();

        }
    }
}
