using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoManager.Domain.Common;

namespace ToDoManager.Persistence.Common
{
    internal class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name);

            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}
