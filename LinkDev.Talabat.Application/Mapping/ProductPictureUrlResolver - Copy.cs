using AutoMapper;
using AutoMapper.Execution;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Shared.Models.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Mapping
{
    class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string?>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string? Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PicutreUrl))
                return $"{_configuration["Urls:ApiBaseUrl"]}{source.PicutreUrl}";
            return string.Empty;
        }
    }

}
