using LinkDev.Talabat.Domain.Contract;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;


namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Lazy<IGenericRepository<Product, int>> _productRepository;
        private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandRepository;
        private readonly Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository;
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
            _brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand , int>(_dbContext));
            _categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));  
        }

        public IGenericRepository<Product, int> ProductRepository => _productRepository.Value;
        public IGenericRepository<ProductBrand, int> Brands => _brandRepository.Value;
        public IGenericRepository<ProductCategory, int> Categories => _categoryRepository.Value;

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
