using LinkDev.Talabat.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Specifications.Orders
{
    public class OrderByPaymentIndentSpecifications:BaseSpecifications<Order,int>
    {

        public OrderByPaymentIndentSpecifications(string paymentIndendId)
            :base(order=>order.PaymentIndentId== paymentIndendId)
        {
            
        }
    }
}
