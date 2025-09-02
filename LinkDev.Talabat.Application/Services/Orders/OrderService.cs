using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Application.Abstraction.Services.Baskets;
using LinkDev.Talabat.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Contract.Persistence;
using LinkDev.Talabat.Domain.Entities.Orders;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Orders
{
    class OrderService(IBasketService basketService
        ,IUnitOfWork unitOfWork
        ,IMapper mapper) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            //1.Get Basket From Basket Repo 
            var basket = await basketService.GetCustomerBasketAsync(order.BasketId);

            //2. Get Selected at Basket From Product Repo 
            var orderItems = new List<OrderItem>();
            
            if (basket.Items.Count() > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();

                foreach(var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if(product is not null)
                    {
                        var ProductItemOrder = new ProductItemOrdered()
                        {
                            ProdutId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PicutreUrl ?? "",
                        };

                        var OrderItem = new OrderItem()
                        {
                            Product = ProductItemOrder,
                            Price = product.Price,
                            Quantity = item.Quantity

                        };
                        orderItems.Add(OrderItem);
                    }
                }

            }


            //3.Calculate SubTotal 

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //4.Mapp Address && Create Order 

            var Address = mapper.Map<Address>(order.ShippingAddress);

            var orderToCreate = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = Address,
                Items=orderItems,
                SubTotal=subTotal,
                DeliveryMethodId=order.DeliveryMethodId
            };
            await unitOfWork.GetRepository<Order,int>().AddAsync(orderToCreate);

            //5.Save to DataBase
            var created = await unitOfWork.CompleteAsync() > 0;
            if (!created)
                throw new BadRequesException("An Error Occcur when created the order");

            return mapper.Map<OrderToReturnDto>(orderToCreate);

        }

        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail);

            var orders = await unitOfWork.GetRepository<Order, int>().GetAllWithSpecAsync(orderSpecs);

            return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }
        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail,orderId);

            var order = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(orderSpecs);

            if (order is null) throw new NotFoundException(nameof(Order), orderId);
            return mapper.Map<OrderToReturnDto>(order);
        }
        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var deliverMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDto>>(deliverMethods);
        }

       

     
    }
}
