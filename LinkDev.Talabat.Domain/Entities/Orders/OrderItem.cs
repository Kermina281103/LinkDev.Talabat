namespace LinkDev.Talabat.Domain.Entities.Orders
{
    public class OrderItem:BaseAuditableEntity<int>
    {
        public required ProductItemOrdered Product { get; set; }
        public decimal  Cost { get; set; }
        public int  Quantity { get; set; }
    }
}
