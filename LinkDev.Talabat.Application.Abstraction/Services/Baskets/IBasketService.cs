using LinkDev.Talabat.Application.Abstraction.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services.Baskets
{
    public interface IBasketService
    {
        Task<BasketDto> GetCustomerBasketAsync(string id);
        Task<BasketDto> UpdateCustomerBasketAsync(BasketDto basket);
        Task DeleteCustomerBasketAsync(string id);
    }
}
