using LinkDev.Talabat.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contract.Infrastructure
{
    public interface IPaymentService
    {
        Task<Basket?> CreateOrUpdateIntent(string BasketId);    
    }
}
