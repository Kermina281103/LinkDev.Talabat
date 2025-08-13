using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Application.Abstraction.Services.Baskets;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Contract.Infrastructure;
using LinkDev.Talabat.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (basket is null) throw new NotFoundException(nameof(Basket), id);

            var mappedBasket =  _mapper.Map<BasketDto>(basket);
            return mappedBasket;
        }

        public async Task<BasketDto> UpdateCustomerBasketAsync(BasketDto basket)
        {
            var updatedBasket = _mapper.Map<Basket>(basket);

            var timeToLive = int.Parse( _configuration.GetSection("RedisSetting")["TimeToLive"]!);
            var updated = await _basketRepository.UpdateAsync(updatedBasket,
                                          TimeSpan.FromDays(timeToLive));

            if (updated is null) throw new BadRequesException("An " +
                "Error has occured , can't updated your basket , please try again ");
            return basket;
        }
    }
}
