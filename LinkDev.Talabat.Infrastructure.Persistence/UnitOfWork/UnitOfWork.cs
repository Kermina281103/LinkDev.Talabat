using LinkDev.Talabat.Domain.Contract;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System.Collections.Concurrent;


namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repostoires;


        /// private readonly Lazy<IGenericRepository<Product, int>> _productRepository;
        /// private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandRepository;
        /// private readonly Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository;
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repostoires = new();

           /// _productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
           /// _brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand , int>(_dbContext));
           /// _categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));  
        }

        /// public IGenericRepository<Product, int> ProductRepository => _productRepository.Value;
        /// public IGenericRepository<ProductBrand, int> Brands => _brandRepository.Value;
        /// public IGenericRepository<ProductCategory, int> Categories => _categoryRepository.Value;

        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
       => await _dbContext.DisposeAsync();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            /// var typeName = typeof(TEntity).Name;
            /// if (_repostoires.ContainsKey(typeName)) return (IGenericRepository<TEntity,TKey>)_repostoires[typeName];
            /// var repository = new GenericRepository<TEntity, TKey>(_dbContext);
            /// _repostoires[typeName] = repository;
            /// _repostoires.Add(typeName, repository);
            /// return repository;
            /// 

            return (IGenericRepository<TEntity, TKey>)_repostoires.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));

        }
    }
}
