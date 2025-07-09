using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Services.Products;
using LinkDev.Talabat.Domain.Contract.Persistence;
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
            private readonly Lazy<IProductService> _productService;

            public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                // Fixed: removed asterisks and used correct field name
                _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
            }

            public IProductService ProductService
            {
                get
                {
                    return _productService.Value;
                }
            }
        }
    }
}
