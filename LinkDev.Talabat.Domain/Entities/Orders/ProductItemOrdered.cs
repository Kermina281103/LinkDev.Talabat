namespace LinkDev.Talabat.Domain.Entities.Orders
{
    public class ProductItemOrdered
    {
        public int ProdutId { get; set; }
        public required string  ProductName { get; set; }
        public required string  PictureUrl { get; set; }

    }
}
