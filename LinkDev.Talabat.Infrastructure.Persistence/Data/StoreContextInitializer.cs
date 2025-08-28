using LinkDev.Talabat.Domain.Contract.Persistence.DbInitializer;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
     class StoreContextInitializer(StoreDbContext _dbContext) :DbInitializer(_dbContext), IStoreDbInitializer
    {
        
      

        public override async Task SeedAsync()
        {
            if (!_dbContext.Brands.Any())
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
                    await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                }
            }


            if (!_dbContext.Categories.Any())
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
                    await _dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
                    await _dbContext.SaveChangesAsync();
                }         
            }


            if (!_dbContext.Products.Any())
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
                    await _dbContext.Set<Product>().AddRangeAsync(products);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
