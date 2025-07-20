using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Services.LinkDev.Talabat.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using LinkDev.Talabat.Application.Mapping;

namespace LinkDev.Talabat.Application
{
    public static  class DependencyInjection
    {
       public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<MappingProfile>();
                // Add more profiles if needed
            });
            services.AddScoped<ProductPictureUrlResolver>();
            //services.AddScoped<IProductService, ProductService>();

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
           
            return services;
        }
    }
}
