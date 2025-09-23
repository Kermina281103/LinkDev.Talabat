using LinkDev.Talabat.Application.Abstraction.Common.Contracts.Baskets;
using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
      
        public IAuthServices AuthService { get; }
        public IOrderService OrderService { get; }
    }
}
