using LinkDev.Talabat.Domain.Contract.Persistence;
using LinkDev.Talabat.Domain.Contract.Persistence.DbInitializer;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            #region StoreInitializer 

            services.AddDbContext<StoreDbContext>((serviceProvider,optionBuilder) =>
          {
              optionBuilder
              .UseLazyLoadingProxies()
              .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
              .AddInterceptors(serviceProvider.GetRequiredService<AuditInterCeptor>());

          });
            services.AddScoped(typeof(AuditInterCeptor));
            services.AddScoped<IStoreDbInitializer, StoreContextInitializer>();
            #endregion

            #region storeIdentityDbInitializer 

            services.AddDbContext<StoreIdentityDbContext>(optionBuilder =>
           {
               optionBuilder
               .UseLazyLoadingProxies()
               .UseSqlServer(configuration.GetConnectionString("IdentityContext"));

           });

            services.AddScoped<IStoreIdentityDbInitializer, StoreIdentityDbInitializer>(); 
            #endregion

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(AuditInterCeptor));
            return services;
        }
    }
}
