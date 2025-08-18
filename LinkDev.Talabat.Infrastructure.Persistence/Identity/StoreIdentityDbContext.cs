using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using LinkDev.Talabat.Infrastructure.Persistence.Identity.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
               type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreIdentityDbContext));
        }
    }


    }

