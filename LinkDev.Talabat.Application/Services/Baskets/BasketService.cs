using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services.Baskets;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Contract.Infrastructure;
using Microsoft.Extensions.Configuration;
using LinkDev.Talabat.Domain.Entities.Baskets;
using LinkDev.Talabat.Shared.Models.Basket;
namespace LinkDev.Talabat.Application.Services.Baskets
{
    public class BasketService : IBasketService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _configuration;

        public BasketService(IBasketRepository basketRepository,IConfiguration configuration,IMapper mapper)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _configuration = configuration;
        }
        public async Task DeleteCustomerBasketAsync(string id)
        {
            await _basketRepository.DeleteAsync(id);
        }

        public async Task<BasketDto> GetCustomerBasketAsync(string id)
        {
            var basket = await _basketRepository.GetAsync(id);
            if (basket is null) throw new NotFoundException(nameof(BasketDto), id);

            var mappedBasket = _mapper.Map<BasketDto>(basket);
            return mappedBasket;
        }

        public async Task<BasketDto> UpdateCustomerBasketAsync(BasketDto basket)
        {
            var timeToLive = int.Parse(_configuration.GetSection("RedisSetting")["TimeToLive"]!);

            var updated = await _basketRepository.UpdateAsync(basket, TimeSpan.FromDays(timeToLive));

            if (updated is null)
                throw new BadRequesException("An error has occurred, can't update your basket, please try again");

            return updated; // This is now safe because we've checked for null above
        }
    }
}
