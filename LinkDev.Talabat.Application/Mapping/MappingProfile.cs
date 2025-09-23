using AutoMapper;
using LinkDev.Talabat.Domain.Entities.Baskets;
using LinkDev.Talabat.Domain.Entities.Employees;
using LinkDev.Talabat.Domain.Entities.Orders;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Shared.Models;
using LinkDev.Talabat.Shared.Models.Basket;
using LinkDev.Talabat.Shared.Models.Employees;
using LinkDev.Talabat.Shared.Models.Orders;
using LinkDev.Talabat.Shared.Models.Products;

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

            CreateMap<Domain.Entities.Identity.Address, AddressDto>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProdutId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
           
            CreateMap<DeliveryMethod, DeliveryMethodDto>();



        
        }
    }
}
