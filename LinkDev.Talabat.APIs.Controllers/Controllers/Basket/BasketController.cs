using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Common.Contracts.Baskets;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Shared.Models.Basket;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Baskets
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;

        }

        [HttpGet]//GET: base/api/basket?id=
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var response = await _basketService.GetCustomerBasketAsync(id);
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> UpdateBasket(BasketDto basketDto)
        {

            return await _basketService.UpdateCustomerBasketAsync(basketDto);


        }
        [HttpDelete]//Delete 
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _basketService.DeleteCustomerBasketAsync(id);

            return NoContent();
        }

    }
}