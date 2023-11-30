using System.Linq.Expressions;

namespace JustLinq.SqlServer
{
    internal class SqlServerExpressionTranslator : ExpressionTranslator
    {
        public override string Translate(Expression expression)
        {
            var visitor = new JustLinqExpressionVisitor();
            visitor.Visit(expression);

            return visitor.TranslatedQuery;
        }
    }
}