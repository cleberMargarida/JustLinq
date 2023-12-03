using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JustLinq.SqlServer.Utils
{
    internal class ExpressionWhereRewrite : ExpressionRewrite
    {
        public ExpressionWhereRewrite(Expression root) : base(root) { }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var methods = node.GetCallStack();

            if (methods.Count(m => m == nameof(Queryable.Where)) < 2)
            {
                return base.VisitMethodCall(node);
            }

            var predicates = node.FindAll<LambdaExpression>();

            if (predicates == null || predicates.Count < 2)
            {
                return base.VisitMethodCall(node);
            }

            var parameter = predicates[0].Find<ParameterExpression>(o => o.StopOnLast);
            var tableExpression = JustLinqExpressionVisitorFactory.Instance.TableVisitor.VisitTable(node);
            var source = tableExpression as Expression ?? node.Find<ParameterExpression>(o => o.StopOnFirst)!;
            var combinedPredicate = CombinePredicates(predicates, parameter);

            var newExpression = Expression.Call(
                node.Method,
                source,
                combinedPredicate
            );

            SetResult(source, newExpression);

            return node;
        }

        private static LambdaExpression CombinePredicates(List<LambdaExpression> predicates, ParameterExpression? parameter)
        {
            var combinedPredicate = Expression.Lambda(
                Expression.AndAlso(predicates[0].Body, predicates[1].Body),
                parameter);

            foreach (var predicate in predicates.Skip(2))
            {
                combinedPredicate = Expression.Lambda(
                Expression.AndAlso(combinedPredicate.Body, predicate.Body),
                parameter);
            }

            return combinedPredicate;
        }
    }
}
