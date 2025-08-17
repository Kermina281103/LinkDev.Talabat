using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Identity
{
    class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {

        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext>options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());

          //  builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
        }
    
    
    }
}
