using LinkDev.Talabat.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Specifications.Orders
{
    public class OrderSpecifications:BaseSpecifications<Order,int>
    {
        private int orderId;

        public OrderSpecifications(string buyerEmail)
            :base(order=>order.BuyerEmail== buyerEmail)
        {
            AddInclude();
            AddOrderByDes(order => order.OrderDate);
        }

        public OrderSpecifications(string buyerEmail, int orderId) :
            base(order=>order.BuyerEmail==buyerEmail&& order.Id==orderId)
        {
            AddInclude(); //Private Method 
        }

        private protected override void AddInclude()
        {
            base.AddInclude();
            Includes.Add(order => order.Items);
            Includes.Add(order => order.DeliveryMethod!);
        }
    }
}
