using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Application.Abstraction.Models.Employees;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Domain.Entities.Basket;
using LinkDev.Talabat.Domain.Entities.Employees;
using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Mapping
{
    public class MappingProfile:Profile
    {
        
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(src => src.Brand!.Name))
                .ForMember(d => d.Category, O => O.MapFrom(src => src.Category!.Name))
                //.ForMember(d=>d.PictureUrl,O=>O.MapFrom(s=>$"{"https://localhost:7097"}{s.PicutreUrl}");
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Employee, EmployeeToReturn>()
                .ForMember(d => d.Department, o => o.MapFrom(src => src.Department!.Name));
        }
    }
}
