

namespace LinkDev.Talabat.Shared.Models.Orders
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
