using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext>options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
                type=>type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType==typeof(StoreDbContext));
          //  modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
   
       
    }
}
