using AutoMapper;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Contract.Infrastructure;
using LinkDev.Talabat.Domain.Contract.Persistence;
using LinkDev.Talabat.Domain.Entities.Orders;
using LinkDev.Talabat.Domain.Specifications.Orders;
using LinkDev.Talabat.Shared.Models;
using LinkDev.Talabat.Shared.Models.Basket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Forwarding;
using Product = LinkDev.Talabat.Domain.Entities.Products.Product;

namespace LinkDev.Talabat.Infrastructure.Payment_service
{
    class PaymentService(IBasketRepository basketRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IOptions<RedisSetting> redisSetting,
        IOptions<StripeSetting> stripeSetting,
        IConfiguration configuration)
             : IPaymentService
    {
        private readonly RedisSetting _redisSetting = redisSetting.Value;
        private readonly StripeSetting _stripeSetting = stripeSetting.Value;
        public async  Task<BasketDto?> CreateOrUpdateIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _stripeSetting.Secretkey;
            var basket= await basketRepository.GetAsync(BasketId);  
            if (basket is null )
                throw new NotFoundException(nameof(BasketDto), BasketId);

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod,int>().GetAsync(basket.DeliveryMethodId.Value);
                if (deliveryMethod is null)
                {
                 throw new NotFoundException(nameof(DeliveryMethod), basket.DeliveryMethodId.Value);
                }
               basket.shippingPrice = deliveryMethod.Cost;
            }

            if (basket.Items.Count()>0)
            {
                var ProductRepo= unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var Product = await ProductRepo.GetAsync(item.Id);
                    if (Product == null)
                        throw new NotFoundException(nameof(Product), item.Id);

                    if (item.Price != Product!.Price)
                        item.Price = Product.Price;
                }
            }

            PaymentIntent? paymentIntent = null;
            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.shippingPrice * 100,
                    Currency="USD",
                    PaymentMethodTypes= new List<string>() { "card"}
                    
                };

                paymentIntent= await  paymentIntentService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.Cleintecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.shippingPrice * 100,

                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);


            }
            await basketRepository.UpdateAsync(basket, TimeSpan.FromDays(_redisSetting.TimeToLive));

            return  mapper.Map<BasketDto>(basket);


        }

        public async Task UpdateOrderPaymentStatus(string request, string header)
        {
            var endPointSecret = configuration.GetSection("StripeStting")["EndPointSecret"];
                var stripeEvent = EventUtility.ConstructEvent(request,
                                   header, endPointSecret,throwOnApiVersionMismatch:false);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    {
                        await UpdatePaymentFailed(paymentIntent!.Id);
                       
                        break;
                    }

                case EventTypes.PaymentIntentSucceeded:
                    {
                        await UpdatePaymentSuccess(paymentIntent!.Id);
                        break;
                    }
                // ... handle other event types
                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }


        }

        private async Task UpdatePaymentSuccess(string id)
        {
            var orderRepo = unitOfWork.GetRepository<Order, int>();
            var order = await orderRepo.GetWithSpecAsync(new OrderByPaymentIndentSpecifications(id))??throw new Exception();

            order.Status = OrderStatus.PaymentFailed;
            orderRepo.Update(order);
            await unitOfWork.CompleteAsync();
        }

        private async Task UpdatePaymentFailed(string id)
        {
            var orderRepo = unitOfWork.GetRepository<Order, int>();
            var order = await orderRepo.GetWithSpecAsync(new OrderByPaymentIndentSpecifications(id)) ?? throw new Exception();

            order.Status = OrderStatus.PaymentReceived;
            orderRepo.Update(order);
            await unitOfWork.CompleteAsync();
        }
    }
}
