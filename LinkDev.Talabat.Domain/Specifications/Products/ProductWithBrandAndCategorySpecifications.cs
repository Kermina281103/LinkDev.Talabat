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
        public ProductWithBrandAndCategorySpecifications(string? sort,int? brandId ,int? categoryId)
            :base( P=>
                 (!brandId.HasValue||P.BrandId==brandId.Value)
                 &&
                 (!categoryId.HasValue|| P.CategoryId==categoryId.Value)
                 
                 
                 )
        {
            AddInclude();

            AddSorting(sort);
        }





        //The spec ojbect creataed via this construcot is for building the query the will get a specific Product 
        public ProductWithBrandAndCategorySpecifications(int id ):base(id)
        {
            AddInclude();

        }


        #region Helper Methods 
        private protected override void AddSorting(string? sort)
        {
            switch (sort)
            {
                case "nameDesc":
                    AddOrderByDes(P => P.Name);
                    break;
                case "priceAsc":
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDesc":
                    AddOrderByDes(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Name);
                    break;
            }
        }

        private protected override void AddInclude()
        {
            base.AddInclude();
            Includes.Add(e => e.Brand!);
            Includes.Add(e => e.Category!);
        } 
        #endregion



    }
}
