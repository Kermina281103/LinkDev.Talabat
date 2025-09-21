using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Entities.Baskets
{
    public class Basket:BaseEntity<string>
    {
        public required IEnumerable<BasketItem> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? Cleintecret { get; set; }
        public decimal shippingPrice { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
