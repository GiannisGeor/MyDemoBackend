using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Model.Extensions
{
    internal static class QueryableExtensions
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int pageIndex, int? pageSize) where T : class
        {
            if (pageSize.HasValue && pageSize.Value > 0)
            {
                pageIndex = Math.Max(pageIndex, 0);
                query = query
                    .Skip(pageIndex * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate) where T : class
        {

            return condition
                ? query.Where(predicate)
                : query;
        }

      
        public static TQueryable WhereIf<T, TQueryable>(this TQueryable query, bool condition, Expression<Func<T, bool>> predicate) where T : class
            where TQueryable : IQueryable<T>
        {

            return condition
                ? (TQueryable)query.Where(predicate)
                : query;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate) where T : class
        {

            return condition
                ? query.Where(predicate)
                : query;
        }
        public static TQueryable WhereIf<T, TQueryable>(this TQueryable query, bool condition, Expression<Func<T, int, bool>> predicate) where T : class
            where TQueryable : IQueryable<T>
        {

            return condition
                ? (TQueryable)query.Where(predicate)
                : query;
        }
    }
}
