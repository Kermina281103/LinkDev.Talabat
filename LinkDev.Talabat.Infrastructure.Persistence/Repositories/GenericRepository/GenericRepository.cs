using LinkDev.Talabat.Domain.Contract;
using LinkDev.Talabat.Domain.Contract.Persistence;
using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.GenericRepository
{
    class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
       
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
            if (typeof(TEntity) == typeof(Product))
                return withTracking ? (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync()
                : (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();

                return withTracking ? await _dbContext.Set<TEntity>().ToListAsync()
                    : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }


        public async  Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> spec, bool withTracking = false)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async  Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> spec)
        {
            return  await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);
        public async Task AddAsync(TEntity entity)
       => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void  Update(TEntity entity)
         => _dbContext.Set<TEntity>().Update(entity);
        public void  Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<int> GetCountAsync(ISpecification<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).CountAsync();   
        }

        #region Help 

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity,TKey> spec)
        {
            return SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);
        }

        #endregion

    }
}
