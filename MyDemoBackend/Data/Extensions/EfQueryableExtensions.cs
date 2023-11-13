using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions
{
    public static class EfQueryableExtensions
    {
        public static IQueryable<T> IncludeIf<T, TProperty>(this IQueryable<T> source, bool condition, Expression<Func<T, TProperty>> path)
            where T : class
        {
            return condition
                ? source.Include(path)
                : source;
        }

        public static IQueryable<T> IncludeIf<T>(this IQueryable<T> source, bool condition, string path)
            where T : class
        {
            return condition
                ? source.Include(path)
                : source;
        }

        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int pageIndex, int? pageSize) where T : class
        {
            if (pageSize.HasValue && pageSize.Value > 0)
            {
                pageIndex = Math.Max(pageIndex, 1);
                query = query
                    .Skip((pageIndex - 1) * pageSize.Value)
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

        /// <summary>
        /// If the condition given is true it orders by Ascending, else Descending.
        /// </summary>
        public static IOrderedQueryable<TSource> OrderByIf<TSource, TKey>(this IQueryable<TSource> query, bool conditionLeadToAsc, Expression<Func<TSource, TKey>> keySelector)
        {
            if (keySelector != null)
            {
                return conditionLeadToAsc
                    ? query.OrderBy(keySelector)
                    : query.OrderByDescending(keySelector);
            }
            else
            {
                return (IOrderedQueryable<TSource>)query;
            }
        }

        /// <summary>
        /// If the condition given is true it orders by Ascending, else Descending.
        /// </summary>
        public static IOrderedQueryable<TSource> ThenOrderByIf<TSource, TKey>(this IOrderedQueryable<TSource> query, bool conditionLeadToAsc, Expression<Func<TSource, TKey>> keySelector)
        {
            if (keySelector != null)
            {
                return conditionLeadToAsc
                    ? query.ThenBy(keySelector)
                    : query.ThenByDescending(keySelector);
            }
            else
            {
                return query;
            }
        }

        /// <summary>
        /// Choose to add AsNoTracking or not based on the parameter given
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="conditionLeadToAsTracking">If true then it will not add AsNoTracking to the query else it adds AsNoTracking()</param>
        /// <returns></returns>
        public static IQueryable<T> AsTrackingIf<T>(this IQueryable<T> query, bool conditionLeadToAsTracking) where T : class
        {

            return conditionLeadToAsTracking
                ? query
                : query.AsNoTracking();
        }

        ///// <summary>
        ///// Dynamic sorting method which can be applied easily to every query
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="query"></param>
        ///// <param name="propertyName">The property name to sort by</param>
        ///// <param name="sortOrder">The sort direction</param>
        ///// <param name="defaultSorting">The default sorting field</param>
        ///// <returns></returns>
        //public static IOrderedQueryable<T> OrderByStringIf<T>(this IQueryable<T> query, string propertyName, SortOrder sortOrder, Expression<Func<T, object>> defaultSorting)
        //{
        //    // If we don't have a propertyName available, just skip sorting
        //    if (String.IsNullOrWhiteSpace(propertyName))
        //    {
        //        return query.OrderByIf(sortOrder == SortOrder.Asc, defaultSorting);
        //    }

        //    var entityType = typeof(T);

        //    // Create x => x.PropName and check if exists
        //    var propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //    if (propertyInfo == null)
        //    {
        //        // Property doesn't exist on class, so we sort by default
        //        return query.OrderBy(defaultSorting);
        //    }

        //    ParameterExpression arg = Expression.Parameter(entityType, "x");
        //    MemberExpression property = Expression.Property(arg, propertyName);
        //    var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

        //    // Decide if we need OrderBy or OrderByDesc
        //    var enumarableType = typeof(Queryable);
        //    MethodInfo method;
        //    if (sortOrder == SortOrder.Asc)
        //    {
        //        // Get System.Linq.Queryable.OrderBy() method,
        //        method = enumarableType.GetMethods()
        //            .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition)
        //            .Where(m =>
        //            {
        //                var parameters = m.GetParameters().ToList();
        //                //Put more restriction here to ensure selecting the right overload                
        //                return parameters.Count == 2;//overload that has 2 parameters
        //            }).Single();
        //    }
        //    else
        //    {
        //        // Get System.Linq.Queryable.OrderByDescending() method,
        //        method = enumarableType.GetMethods()
        //            .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition)
        //            .Where(m =>
        //            {
        //                var parameters = m.GetParameters().ToList();
        //                //Put more restriction here to ensure selecting the right overload                
        //                return parameters.Count == 2;//overload that has 2 parameters
        //            }).Single();
        //    }



        //    // The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        //    MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

        //    // Call query.OrderBy(selector), with query and selector: x => x.PropName
        //    // Note that we pass the selector as Expression to the method and we don't compile it.
        //    // By doing so EF can extract "order by" columns and generate SQL for it.
        //    var newQuery = (IOrderedQueryable<T>)genericMethod
        //        .Invoke(genericMethod, new object[] { query, selector });

        //    return newQuery;
        //}

    }
}
