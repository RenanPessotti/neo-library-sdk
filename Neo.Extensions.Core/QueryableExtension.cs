using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Neo.Extensions.Core
{
    public static class QueryableExtension
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> q, string sortField, bool ascending)
        {
            try
            {
                var propertyNames = sortField.Split(".");

                ParameterExpression pe = Expression.Parameter(typeof(T), string.Empty);
                Expression property = pe;
                foreach (var prop in propertyNames)
                {
                    property = Expression.PropertyOrField(property, prop);
                }

                LambdaExpression lambda = Expression.Lambda(property, pe);
                string method = ascending ? "OrderBy" : "OrderByDescending";
                Type[] types = new Type[] { q.ElementType, lambda.Body.Type };
                MethodCallExpression mce = Expression.Call(
                    typeof(Queryable),
                    method,
                    types,
                    q.Expression,
                    Expression.Quote(lambda));

                return q.Provider.CreateQuery<T>(mce);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
