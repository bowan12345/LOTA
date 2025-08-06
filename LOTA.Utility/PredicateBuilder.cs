using System.Linq.Expressions;

namespace LOTA.Utility
{
    public static class PredicateBuilder
    {
        // SQL: WHERE 1=1 AND TutorId = ... AND CourseName LIKE ...
        public static Expression<Func<T, bool>> True<T>() { return param => true; }

        // SQL: WHERE 1=0 OR TutorId = ... OR CourseName LIKE ...
        public static Expression<Func<T, bool>> False<T>() { return param => false; }

        // AND conditions
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var combined = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        // OR conditions
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var combined = Expression.OrElse(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }
    }
}
