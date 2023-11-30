using JustLinq.SqlServer.Utils;
using System.Linq.Expressions;

namespace JustLinq.SqlServer
{

    internal partial class JustLinqExpressionVisitor : IExpressionPrinter
    {
        internal string Print(Expression expression)
        {
            Visit(expression);
            return _stringBuilder.ToString().FormatSql();
        }

        #region interface methods
        Expression? IExpressionPrinter.Visit(Expression? expression)
        {
            return this.Visit(expression);
        }

        string IExpressionPrinter.Print(Expression expression)
        {
            return this.Print(expression);
        }
        #endregion
    }
}
