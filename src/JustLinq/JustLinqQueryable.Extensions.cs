using System.Linq;

namespace JustLinq
{
    public static class JustLinqQueryableExtensions
    {
        public static string ToQueryString(this IQueryable query)
            => ExpressionTranslator.Shared.Translate(query.Expression);

#if DEBUG
        public static string? ToQueryString(this object? _)
            => ExpressionTranslator.Shared.NextQuery;
#endif
    }
}
