using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Abstraction.Services.Baskets;
using LinkDev.Talabat.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Services.Employees;
using LinkDev.Talabat.Application.Services.Products;
using LinkDev.Talabat.Domain.Contract.Persistence;
using Microsoft.Extensions.Configuration;

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
            private readonly Lazy<IAuthServices> _authService;
            private readonly Lazy<IOrderService> _orderService;

            public ServiceManager(IUnitOfWork unitOfWork, IConfiguration configuration ,IMapper mapper,Func<IOrderService> orderService,Func<IBasketService> basketServiceFactory, Func<IAuthServices> authServiceFactor)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
                _mapper = mapper;
                // Fixed: removed asterisks and used correct field name
                _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
               _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService (_unitOfWork,_mapper));
                _orderService = new Lazy<IOrderService>(orderService, LazyThreadSafetyMode.ExecutionAndPublication);
                _basketService = new Lazy<IBasketService>(basketServiceFactory,LazyThreadSafetyMode.ExecutionAndPublication);
                _authService = new Lazy<IAuthServices>(authServiceFactor,LazyThreadSafetyMode.ExecutionAndPublication);
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

            public IOrderService OrderService => _orderService.Value;
            public IBasketService BasketService => _basketService.Value;
            public IAuthServices AuthService => _authService.Value;
        }
    }
}
