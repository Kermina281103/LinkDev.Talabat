using LinkDev.Talabat.Domain.Contract.Persistence.DbInitializer;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static  class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateAsyncScope();
            var Services = Scope.ServiceProvider;
            var StoreContextInitializer = Services.GetRequiredService<IStoreDbInitializer>();
            var IdentityContextInitializer = Services.GetRequiredService<IStoreIdentityDbInitializer>();
            //Ask Runtime Env for an object from "storeDbContext" service Explicity 

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

            //Apply Migrations 
            try
            {
               await StoreContextInitializer.InitializeAsync();
                await StoreContextInitializer.SeedAsync();
                await IdentityContextInitializer.InitializeAsync();
                await IdentityContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error has been occured during apply the migraion or the Data Seeding ");
            }
            return app;
        }
    }
}
