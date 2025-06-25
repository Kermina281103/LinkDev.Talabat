using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public static class StoreContextSeeds
    {
        public static async  Task SeedAsync(StoreDbContext dbContext)
        {
            if (!dbContext.Brands.Any())
            {

                var brandData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/Brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    

                if (brands?.Count > 0)
                {
                    /// foreach (var brand in brands)
                    /// {
                    ///     await dbContext.Brands.AddAsync(brand);
                    ///
                    /// }
                    /// 
                    await dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                     await dbContext.SaveChangesAsync();
                }
            }


            if (!dbContext.Categories.Any())
            {

                var categoryData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/Categories.json");

                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);


                if (Categories?.Count > 0)
                {
                    /// foreach (var brand in brands)
                    /// {
                    ///     await dbContext.Brands.AddAsync(brand);
                    ///
                    /// }
                    /// 
                    await dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
                    await dbContext.SaveChangesAsync();
                }
            }


            if (!dbContext.Products.Any())
            {

                var ProductData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/Products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);


                if (products?.Count > 0)
                {
                    /// foreach (var brand in brands)
                    /// {
                    ///     await dbContext.Brands.AddAsync(brand);
                    ///
                    /// }
                    /// 
                    await dbContext.Set<Product>().AddRangeAsync(products);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
