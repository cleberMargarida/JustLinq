using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    internal static class EnumerableExtensions
    {
        public static T LastButOne<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 1)
            {
                return source.First();
            }

            return source.Reverse().Skip(1).First(); 
        }
        
        public static T LastButOneOrDefault<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 1)
            {
                return source.FirstOrDefault();
            }

            return source.Reverse().Skip(1).FirstOrDefault(); 
        }

        public static TExpression Cast<TExpression>(this Expression expression) where TExpression : Expression
        {
            return (TExpression)expression;
        }
    }
}
