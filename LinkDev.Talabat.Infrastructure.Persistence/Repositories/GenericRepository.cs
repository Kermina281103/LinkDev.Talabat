using LinkDev.Talabat.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
       
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
            if (withTracking) return await _dbContext.Set<TEntity>().ToListAsync();

            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(TKey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);
        public async Task AddAsync(TEntity entity)
     => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void  Update(TEntity entity)
         => _dbContext.Set<TEntity>().Update(entity);
        public void  Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

      

       

     
    }
}
