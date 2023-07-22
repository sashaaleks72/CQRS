using System.Linq.Expressions;

namespace Modules.Test.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByParams<T>(this IQueryable<T> query, string? sort, bool? isAsc)
        {
            var parameter = Expression.Parameter(typeof(T), "t");

            if (!string.IsNullOrEmpty(sort)) 
            {
                sort = $"{sort[0].ToString().ToUpper()}{sort.Substring(1)}";
            }

            var property = sort == null
                    ? Expression.Property(parameter, "Name")
                    : Expression.Property(parameter, sort);

            var objectProperty = Expression.TypeAs(property, typeof(object));
            var sortSelector = Expression.Lambda<Func<T, object>>(objectProperty, parameter);

            if (isAsc != null)
            {
                query = isAsc.Value
                ? query.OrderBy(sortSelector)
                : !isAsc.Value
                ? query.OrderByDescending(sortSelector)
                : query;
            }
            
            
            return query;
        }
    }
}