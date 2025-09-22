using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Application.Abstraction.Common;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
   public class ProductController(IServiceManager serviceManager):BaseApiController
    {
      // [Authorize]
        [HttpGet]  //Get :/api/Product
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var products = await serviceManager.ProductService.GetProductsAsync(specParams);
            return Ok(products);
        }

        [HttpGet("{id:int}")] //Get:/api/product/id
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id )
        {
            var response = await serviceManager.ProductService.GetProductAsync(id);
            return Ok(response);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<BrandDto>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }
        

        [HttpGet("categories")]
        public async Task<ActionResult<CategoryDto>> GetCategories()
        {
            var categorires = await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(categorires);
        }
    }
}
