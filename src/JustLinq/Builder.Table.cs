using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ColumnName = System.String;

namespace JustLinq
{
    public class TableBuilder<T> : Builder
    {
        private string? tableName;

        public TableBuilder()
        {
            ColumnsMap = new Dictionary<MemberInfo, ColumnName>();
        }

        internal Dictionary<MemberInfo, ColumnName> ColumnsMap { get; }

        public ColumnBuilder<T> Column(Expression<Func<T, object>> property)
        {
            return new ColumnBuilder<T>(property, this);
        }

        public void Name(string tableName)
        {
            this.tableName = tableName;
        }

        internal Table<T> Build() => new Table<T>
        {
            TableName = tableName ?? typeof(T).Name,
            ColumnsMap = ColumnsMap
        };
    }
}