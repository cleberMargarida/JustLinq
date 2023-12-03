using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JustLinq.SqlServer.Utils
{
    public abstract class ExpressionRewrite : ExpressionVisitor
    {
        protected readonly Expression _root;
        protected Expression? _result;

        public ExpressionRewrite(Expression root)
        {
            _root = root;
        }

        internal static MethodCallExpression Rewrite<T>(MethodCallExpression node)
            where T : ExpressionRewrite
        {
            var instance = (T)Activator.CreateInstance(typeof(T), node);
            instance.Visit(node);
            return (instance._result ?? instance._root).Cast<MethodCallExpression>();
        }

        protected virtual void SetResult(Expression source, MethodCallExpression newExpression)
        {
            if (_root is MethodCallExpression)
            {
                _result = newExpression;
            }

            if (_root is LambdaExpression)
            {
                _result = Expression.Lambda(
                     newExpression,
                     (ParameterExpression)source);
            }
        }
    }
}
