using System.Linq;
using System.Linq.Expressions;

namespace JustLinq.SqlServer.Utils
{
    internal class ExpressionOrderByRewrite : ExpressionRewrite
    {
        public ExpressionOrderByRewrite(Expression root) : base(root) { }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var methods = node.GetCallStack();

            if (methods.Length > 2)
            {
                return base.VisitMethodCall(node);
            }

            var predicates = node.FindAll<LambdaExpression>();

            if (predicates == null || predicates.Count < 2)
            {
                return base.VisitMethodCall(node);
            }

            var tableExpression = (ConstantExpression?)JustLinqExpressionVisitorFactory
                .Instance
                .TableVisitor
                .VisitTable(node)!; 
            
            var source = tableExpression as Expression ?? node.Find<ParameterExpression>(o => o.StopOnFirst)!;
            var orderBy = node.Find<MethodCallExpression>(o => o.StopOnLast)!;
            var where = node.Find<MethodCallExpression>(o => o.StopOnFirst)!;

            var arg0 = Expression.Call(
                where.Method,
                source,
                predicates[1]
            );

            var newExpression = Expression.Call(
                orderBy.Method,
                arg0,
                predicates[0]
            );

            SetResult(source, newExpression);

            return node;
        }
    }
}
