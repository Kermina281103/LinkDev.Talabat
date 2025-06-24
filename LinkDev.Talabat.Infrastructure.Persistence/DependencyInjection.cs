
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddpersistenceService(this IServiceCollection services)
        {
            services.AddDbContext<StoreDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer("DefaultConnection");
            } 
               );

            return services;

        }
    }
}
