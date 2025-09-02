using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Abstraction.Models.Orders
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public required string Status { get; set; }
        public required AddressDto ShippingAddress { get; set; }

        public int? DeliveryMethodId { get; set; }
        public virtual string? DeliveryMethod { get; set; }


        public virtual ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }


    }
}
