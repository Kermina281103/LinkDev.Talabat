using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Services.LinkDev.Talabat.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using LinkDev.Talabat.Application.Mapping;
using LinkDev.Talabat.Application.Services.Baskets;
using Microsoft.Extensions.Configuration;
using LinkDev.Talabat.Domain.Contract.Infrastructure;
using LinkDev.Talabat.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Application.Services.Orders;
using LinkDev.Talabat.Application.Abstraction.Common.Contracts.Baskets;

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
            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            //services.AddScoped(typeof(Func<IBasketService>), (ServiceProvider) =>
            //{
            //    var _mapper = ServiceProvider.GetRequiredService<IMapper>();
            //    var _configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            //    var _basketRepository = ServiceProvider.GetRequiredService<IBasketRepository>();

            //    return () => new BasketService(_basketRepository, _configuration, _mapper);
            //});

            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(Func<IOrderService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IOrderService>();
            });
            return services;
        }
    }
}
