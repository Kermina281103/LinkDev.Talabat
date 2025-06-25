using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public IGenericRepository<Product,int> ProductRepository { get; }
        public IGenericRepository<ProductBrand,int> Brands { get; }
        public IGenericRepository<ProductCategory,int> Categories { get; }


        Task<int> CompleteAsync();
    }
}
