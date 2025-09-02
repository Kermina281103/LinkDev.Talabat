using LinkDev.Talabat.Domain.Entities.Orders;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Orders
{
    class DeliveryMethodConfigurations:BaseEntityConfiguration<DeliveryMethod,int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);

            builder.Property(method => method.Cost)
                .HasColumnType("decimal(8,2)");

        }
    }
}
