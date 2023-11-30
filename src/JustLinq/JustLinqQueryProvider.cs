using System;
using System.Linq;
using System.Linq.Expressions;

namespace JustLinq
{
    internal class JustLinqQueryProvider : IQueryProvider
    {
        private readonly IExecuteQuery _executeQuery;

        public JustLinqQueryProvider(IExecuteQuery executeQuery) =>
            _executeQuery = executeQuery;

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
            new JustLinqQueryable<TElement>(this, expression);

        public IQueryable CreateQuery(Expression expression) =>
            this.CreateQuery(expression.Type, expression);

        public TResult Execute<TResult>(Expression expression) =>
            Execute<TResult>((MethodCallExpression)expression);

        public object Execute(Expression expression) =>
            this.Execute(expression.Type, expression);

        private TResult Execute<TResult>(MethodCallExpression expression)
        {
            var query = ExpressionTranslator.Shared.Translate(expression);
            var result = _executeQuery.GetResult<TResult>(query);

            return ResultCanBeDefault(expression.Method.Name) ?
                result! :
                result ?? throw new InvalidOperationException();
        }

        private static bool ResultCanBeDefault(string methodName) => methodName switch
        {
            nameof(Enumerable.FirstOrDefault) => true,
            nameof(Enumerable.LastOrDefault) => true,
            nameof(Enumerable.SingleOrDefault) => true,
            _ => false
        };
    }
}