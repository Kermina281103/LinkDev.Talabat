using LinkDev.Talabat.Application.Abstraction.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);

        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);
        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);

        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();
    }
}
