using AutoMapper;
using LinkDev.Talabat.Domain.Entities.Orders;
using LinkDev.Talabat.Shared.Models.Orders;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Application.Mapping
{
    class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string?>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{_configuration["Urls:ApiBaseUrl"]}{source.Product.PictureUrl}";
            return string.Empty;
        }
    }

}
