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
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>>? OrderByDes { get; set; } = null;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 0;
        public bool IsPaginationEnable { get; set; } = false;

        public BaseSpecifications(TKey id)
        {
            Criateria = E => E.Id.Equals(id);
        }

        protected BaseSpecifications()
        {
            
        }

        protected BaseSpecifications(Expression<Func<TEntity,bool>> CriteriaExpresion)
        {
            Criateria = CriteriaExpresion;   
        }

        private protected  virtual void AddOrderBy(Expression<Func<TEntity,object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        private protected virtual void AddOrderByDes(Expression<Func<TEntity,object>> OrderByExpressionDes)
        {
            OrderByDes = OrderByExpressionDes;
        }


        #region Helper Methods 
        private protected virtual void AddInclude()
        {

        }

        private protected virtual void AddSorting(string? sort)
        {

        } 
       private protected virtual void AddPagiation(int skip ,int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }
        #endregion

    }
}
