using LinkDev.Talabat.Application.Abstraction.Common.Contracts.Infrastructure;
using LinkDev.Talabat.Domain.Contract.Infrastructure;
using LinkDev.Talabat.Infrastructure.Basket_Repository;
using LinkDev.Talabat.Infrastructure.Payment_service;
using LinkDev.Talabat.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static  IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration config)
        {
            
            services.AddScoped(typeof(IConnectionMultiplexer), (serviceProvider) =>
            {
                var connectionString = config.GetConnectionString("Redis");

                var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString);
                return connectionMultiplexerObj;
            });
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            services.Configure<RedisSetting>(config.GetSection("RedisSetting"));
            services.Configure<StripeSetting>(config.GetSection("StripeStting"));
            return services;
        }
    }
}
