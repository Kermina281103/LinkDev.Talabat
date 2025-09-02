using LinkDev.Talabat.Domain.Entities.Orders;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Orders
{
    class OrderItemConfigurations:BaseAuditableEntityConfiguration<OrderItem,int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(item => item.Product, Product => Product.WithOwner());

            builder.Property(item => item.Price)
                .HasColumnType("decimal(8,2)");
        }
    }
}
