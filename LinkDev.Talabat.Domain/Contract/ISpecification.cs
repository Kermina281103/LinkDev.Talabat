using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contract
{
    public interface ISpecification<TEntity,TKey>
        where TEntity:BaseEntity<TKey>
        where TKey:IEquatable<TKey>
    {
        public Expression<Func<TEntity,bool>>? Criateria { get; set; }

        public List<Expression<Func<TEntity,object>>> Includes { get; set; }

        public Expression<Func<TEntity,object>>? OrderBy { get; set; }

        public Expression<Func<TEntity,object>>? OrderByDes { get; set; }


    }
}
