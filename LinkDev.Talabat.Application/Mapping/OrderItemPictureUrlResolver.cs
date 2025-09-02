using AutoMapper;
using AutoMapper.Execution;
using LinkDev.Talabat.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Domain.Entities.Orders;
using LinkDev.Talabat.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
