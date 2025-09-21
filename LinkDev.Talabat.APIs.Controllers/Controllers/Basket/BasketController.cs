using LinkDev.Talabat.APIs.Controllers.Base;

using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Shared.Models.Basket;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Baskets
{
    public class BasketController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public BasketController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;

        }

        [HttpGet]//GET: base/api/basket?id=
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var response = await _serviceManager.BasketService.GetCustomerBasketAsync(id);
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> UpdateBasket(BasketDto basketDto)
        {

            return await _serviceManager.BasketService.UpdateCustomerBasketAsync(basketDto);


        }
        [HttpDelete]//Delete 
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteCustomerBasketAsync(id);

            return NoContent();
        }

    }
}