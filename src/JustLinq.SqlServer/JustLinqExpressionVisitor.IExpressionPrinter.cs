using System.Linq.Expressions;

namespace JustLinq.SqlServer
{
    public interface IExpressionPrinter
    {
        Expression? Visit(Expression? expression);
        internal string Print(Expression expression);
    }
}
