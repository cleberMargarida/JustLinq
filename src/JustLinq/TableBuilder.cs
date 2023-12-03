using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ColumnName = System.String;

namespace JustLinq
{
    public class TableBuilder<T>
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

        public void HasName(string tableName)
        {
            this.tableName = tableName;
        }

        internal Table<T> Build() => new Table<T>
        {
            TableName = tableName ?? typeof(T).Name,
            ColumnsMap = ColumnsMap
        };
    }

    public class ColumnBuilder<T>
    {
        private Expression<Func<T, object>> property;
        private readonly TableBuilder<T> tableBuilder;

        public ColumnBuilder(Expression<Func<T, object>> property, TableBuilder<T> tableBuilder)
        {
            this.property = property;
            this.tableBuilder = tableBuilder;
        }

        public void HasName(string columnName)
        {
            var visitor = new MemberGetter();
            visitor.Visit(property.Body);
            var member = visitor.GetMember()?.Member;

            if (member != null)
            {
                tableBuilder.ColumnsMap[member] = columnName;
            }
        }

        class MemberGetter : ExpressionVisitor
        {
            private MemberExpression? _node;

            protected override Expression VisitMember(MemberExpression node)
            {
                return _node ??= node;
            }

            public MemberExpression? GetMember()
            {
                return _node;
            }
        }
    }
}