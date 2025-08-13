using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Baskets;
using LinkDev.Talabat.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Services.Baskets;
using LinkDev.Talabat.Application.Services.Employees;
using LinkDev.Talabat.Application.Services.Products;
using LinkDev.Talabat.Domain.Contract.Persistence;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services
{

    namespace LinkDev.Talabat.Application.Services
    {
        public class ServiceManager : IServiceManager
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IConfiguration _configuration;
            private readonly Lazy<IProductService> _productService;
            private readonly Lazy<IEmployeeService> _employeeService;
            private readonly Lazy<IBasketService> _basketService;

            public ServiceManager(IUnitOfWork unitOfWork, IConfiguration configuration ,IMapper mapper,Func<IBasketService> basketServiceFactory)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
                _mapper = mapper;
                // Fixed: removed asterisks and used correct field name
                _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
               _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService (_unitOfWork,_mapper));
                _basketService = new Lazy<IBasketService>(basketServiceFactory);
            }

            public IProductService ProductService
            {
                get
                {
                    return _productService.Value;
                    
                }
            }

           public IEmployeeService employeeService

            {
                get
                {
                    return _employeeService.Value;

                }
            }

            public IBasketService BasketService { get => throw new NotImplementedException();}
        }
    }
}
