using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products
{
    public class ProductConfiguration : BaseAuditableEntityConfiguration<Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(P => P.Price)
                .HasColumnType("decimal(9,2)");


            builder.HasOne(product => product.Brand)
                .WithMany()
                .HasForeignKey(Product => Product.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(product => product.Category)
                .WithMany()
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }

}
