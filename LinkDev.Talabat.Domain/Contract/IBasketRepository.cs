using LinkDev.Talabat.Domain.Entities.Baskets;
using LinkDev.Talabat.Shared.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contract.Infrastructure
{
    public interface IBasketRepository
    {
        Task<BasketDto?> GetAsync(string id);
        Task<BasketDto?> UpdateAsync(BasketDto basket, TimeSpan timeToLive);
        Task DeleteAsync(string id);
    }
}
