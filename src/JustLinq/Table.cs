using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JustLinq
{
    public class Table : IQueryable, IOrderedQueryable
    {
        public string TableName { get; set; } = default!;
        public Type ElementType => throw new NotImplementedException();
        public Expression Expression => throw new NotImplementedException();
        public IQueryProvider Provider => throw new NotImplementedException();
        public IEnumerator GetEnumerator() => throw new NotImplementedException();
    }

    public class Table<T> : Table, IQueryable<T>, IOrderedQueryable<T>
    {
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw new NotImplementedException();
    }
}