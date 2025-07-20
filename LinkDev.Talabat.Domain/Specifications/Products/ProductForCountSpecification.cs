using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Specifications.Products
{
    public class ProductForCountSpecification:BaseSpecifications<Product,int >
    {
        public ProductForCountSpecification(int? BrandId,int? CategoryId,string? search):base
            (P=>
            (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
            &&
            (!BrandId.HasValue||BrandId.Value==P.BrandId)
            &&
            (!CategoryId.HasValue||CategoryId.Value==P.CategoryId)
            )
        {
            
        }
    }

}
