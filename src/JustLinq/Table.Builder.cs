using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JustLinq
{
    public class TableBuilder<T>
    {
        List<(Expression<Func<T, object>>?, string?)?> columns = new List<(Expression<Func<T, object>>?, string?)?>();

        public TableBuilder<T> Column(Expression<Func<T, object>> property, string column)
        {
            this.columns.Add((property, column));
            return this;
        }

        public Table<T> Build()
        {
            return new Table<T>();
        }
    }
}