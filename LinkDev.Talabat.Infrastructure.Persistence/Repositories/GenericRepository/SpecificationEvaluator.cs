using LinkDev.Talabat.Domain.Contract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.GenericRepository
{
    static class  SpecificationEvaluator<TEntity,TKey>
        where TEntity:BaseEntity<TKey>
        where TKey:IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity,TKey> spec)
        {
            var query = inputQuery; //dbContext.Set<Product>();
           
            if (spec.Criateria is not null) //P=>P.Id.Equals(id)
                query=query.Where(spec.Criateria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDes is not null)
                query = query.OrderByDescending(spec.OrderByDes);

            ///query =_dbConext.Set<Product>.Where(p=>p.Id.Equals(id))
            ///1.p=>p.Brand
            ///2.p=>p.Category
            ///...
            ///..
          
            query = spec.Includes.Aggregate(query, (currenctExpression, includExpression) =>currenctExpression.Include(includExpression));



            return query;

        }
    }
}
