using System;
using System.Linq.Expressions;

namespace JustLinq
{
    internal abstract class ExpressionTranslator : IExpressionTranslator
    {
        private static ExpressionTranslator? shared;

        public static ExpressionTranslator Shared
        {
            get => shared ?? throw new InvalidOperationException("ExpressionTranslator.Shared needs be configured.");
            set => shared = value;
        }

        public abstract string Translate(Expression expression);
    }
}
