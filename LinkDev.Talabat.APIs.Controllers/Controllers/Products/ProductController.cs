using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services;
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
        [HttpGet]  //Get :/api/Product

        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var products = await serviceManager.ProductService.GetProductsAsync(specParams);
            return Ok(products);
        }

        [HttpGet("{id:int}")] //Get:/api/product/id
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id )
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            if (product is null)
                return NotFound(new { StatusCode = 404, message = "Not found." });
            return Ok(product);
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
