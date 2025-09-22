using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Shared.Models.Basket;
using LinkDev.Talabat.Domain.Contract.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Payment
{
    ///  [Authorize]

    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent([FromRoute] string basketId)
        {
            var result = await _paymentService.CreateOrUpdateIntent(basketId);
            if (result == null) return BadRequest("Problem with your basket");
            return Ok(result);
        }
    


      //const string endpointSecret = "whsec_...";

        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            //var stripeEvent = EventUtility.ConstructEvent(json,
            //    Request.Headers["Stripe-Signature"], endpointSecret);

           await  _paymentService.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]);
            return new EmptyResult();



        }
    }
}
