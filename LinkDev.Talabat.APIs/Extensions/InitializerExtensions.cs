using LinkDev.Talabat.Domain.Contract;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static  class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateAsyncScope();
            var Services = Scope.ServiceProvider;
            var StoreContextInitializer = Services.GetRequiredService<IStoreContextInitializer
                >();
            //Ask Runtime Env for an object from "storeDbContext" service Explicity 

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

            //Apply Migrations 
            try
            {
               await StoreContextInitializer.InitializeAsync();
                await StoreContextInitializer.SeedAsync();
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
