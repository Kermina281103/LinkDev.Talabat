


using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base
{
    public  class BaseAuditableEntityConfiguration<TEntity,TKey>:BaseEntityConfiguration<TEntity,TKey>
     where TEntity:BaseAuditableEntity<TKey>
     where TKey:IEquatable<TKey>
       
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

          /// builder.Property(e => e.CreatedOn)
          ///     .HasDefaultValueSql("GETUTCDATE()");
          ///
          /// builder.Property(e => e.LastModifiedOn)
          ///     .HasComputedColumnSql("GETUTCDATE()");

        }

    }
}
