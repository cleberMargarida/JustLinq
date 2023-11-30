using System.Linq.Expressions;

namespace JustLinq.SqlServer
{
    internal class SqlServerExpressionTranslator : ExpressionTranslator
    {
        public override string Translate(Expression expression)
        {
            return new JustLinqExpressionVisitor().Print(expression);
        }
    }
}