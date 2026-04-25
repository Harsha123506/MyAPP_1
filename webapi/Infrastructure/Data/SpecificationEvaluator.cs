using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Data
{
    internal class SpecificationEvaluator<T> : BaseSpecification<T>
    {
        public SpecificationEvaluator(Expression<Func<T, bool>> criteria) : base(criteria)
        {
        }

        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            if(spec?.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.orderBy != null)
            {
                query = query.OrderBy(spec.orderBy);
            }else if(spec.orderByDesc != null){
                query = query.OrderByDescending(spec.orderByDesc);
            }
            return query;
        }

        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
        {
            if (spec?.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.orderBy != null)
            {
                query = query.OrderBy(spec.orderBy);
            }
            else if (spec.orderByDesc != null)
            {
                query = query.OrderByDescending(spec.orderByDesc);
            }
            var result = query as IQueryable<TResult>;

            if (spec.Select != null)
            {
                result =  query.Select(spec.Select);
            }
            if(spec.IsDistinct)
            {
                result =  result?.Distinct().Cast<TResult>();
            }
            return result ?? query.Cast<TResult>();
        }

        }
}
