using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Shared.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Orders
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager):BaseApiController
    {
        [HttpPost]//Post : /api/Order
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderDto);
            return Ok(result);

        }

        [HttpGet] //Get: /api/Order
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrderForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail!);
            return Ok(result);
        }

        [HttpGet("{id}")] //Get: /api/Orders/{id}

        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrderByIdAsync(buyerEmail!, id);
            return Ok(result);
        }


        [HttpGet("deliveryMethod")] //Get: /api/Orders/{id}

        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethod()
        {
           
            var result = await serviceManager.OrderService.GetDeliveryMethodAsync();
            return Ok(result);
        }
    }
}
