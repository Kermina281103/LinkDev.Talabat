using LinkDev.Talabat.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criateria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();

        public BaseSpecifications()
        {
           // Criateria = null;//Default 
        }
        public BaseSpecifications(TKey id)
        {
            Criateria = E => E.Id.Equals(id);
        }

    }
}
