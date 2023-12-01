using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JustLinq
{
    internal class JustLinqQueryable<T> : IJustLinqOrderByQueryable<T>
    {
        public Type ElementType => typeof(T);
        public Expression? Expression { get; }
        public IQueryProvider Provider { get; }

        private JustLinqQueryable(IQueryProvider provider)
        {
            this.Provider = provider;
        }

        public JustLinqQueryable(IQueryProvider provider, IQueryable<T> innerSource) : this(provider)
        {
            this.Expression = Expression.Constant(innerSource);
        }

        public JustLinqQueryable(IQueryProvider provider, Expression expression) : this(provider)
        {
            this.Expression = expression;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Provider.Execute<IEnumerable<T>>(this.Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
