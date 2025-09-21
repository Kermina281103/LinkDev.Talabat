using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Domain.Contract.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Payment
{
    [Authorize]
    public class PaymentController(IPaymentService paymenetService) : BaseApiController
    {
        [HttpPost("{basketId}")]//Post : /api/payment/(basketId)

        public async Task<ActionResult<BasketDto>> CreateOrUpatePaymentIntent(string basketId)
        {
            var result = await paymenetService.CreateOrUpdateIntent(basketId);
            return Ok(result);
        }

    }
}
