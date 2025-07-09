using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications:BaseSpecifications<Product,int>
    {
        //The object created via this constructor is used for building the query that will Get all porduct 
        public ProductWithBrandAndCategorySpecifications():base()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }


    }
}
