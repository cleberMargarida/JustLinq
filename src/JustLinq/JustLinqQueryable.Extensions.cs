using System.Linq;

namespace JustLinq
{
    public static class JustLinqQueryableExtensions
    {
        public static string ToQueryString(this IQueryable query)
        {
            return ExpressionTranslator.Shared.Translate(query.Expression);
        }
    }
}
