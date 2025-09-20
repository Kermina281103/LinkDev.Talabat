using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Basket
{
    public record BasketDto
    {
        public required string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
       public string? PaymentIntentId { get; set; }
        public string? Cleintecret { get; set; }

        public decimal shippingPrice { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
