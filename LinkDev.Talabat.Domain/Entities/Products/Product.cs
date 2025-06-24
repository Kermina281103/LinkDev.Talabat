

namespace LinkDev.Talabat.Domain.Entities.Products
{
   public  class Product:BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PicutreUrl { get; set; }
        public decimal Price  { get; set; }

        public  int  CategoryId  { get; set; }//Foreign key for product category Entity
        public virtual  ProductCategory? Category { get; set; }

        public  int  BrandId  { get; set; } //Foregin Key for ProductBrand Entity 
        public virtual ProductBrand? Brand { get; set; }

    }
}
