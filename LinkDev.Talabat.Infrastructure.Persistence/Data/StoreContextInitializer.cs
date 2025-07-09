using LinkDev.Talabat.Domain.Contract.Persistence;
using LinkDev.Talabat.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public class StoreContextInitializer(StoreDbContext _dbContext) : IStoreContextInitializer
    {
        
        public async Task InitializeAsync()
        {
            var PendingMigration = await _dbContext.Database.GetPendingMigrationsAsync();

            if (PendingMigration.Any())
                await _dbContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
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
