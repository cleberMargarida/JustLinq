using System;
using System.Linq;
using System.Linq.Expressions;

namespace JustLinq
{
    public class ColumnBuilder<T> : Builder
    {
        private Expression<Func<T, object>> property;
        private readonly TableBuilder<T> tableBuilder;

        public ColumnBuilder(Expression<Func<T, object>> property, TableBuilder<T> tableBuilder)
        {
            this.property = property;
            this.tableBuilder = tableBuilder;
        }

        public ColumnBuilder<T> Name(string columnName)
        {
            var visitor = new MemberGetter();
            visitor.Visit(property.Body);
            var member = visitor.MemberExpression?.Member;

            if (member != null)
            {
                tableBuilder.ColumnsMap[member] = columnName;
            }

            return this;
        }

        public ColumnBuilder<T> SqlType(string sqlType)
        {
            //TODO: create a hashmap between member and sqlType.
            return this;
        }

        public ColumnBuilder<T> PrimaryKey()
        {
            //TODO: create a hashmap between table and PK's members
            return this;
        }

        public ColumnBuilder<T> ForeignKey<T2>(T2 externalTable)
            where T2 : IQueryable
        {
            //TODO: create a hashmap between tuple (table, member) and external table.
            return this;
        }

        [Obsolete("prefer exclude the property using .Select")]
        public ColumnBuilder<T> Skip()
        {
            //TODO: create a hasmap between tables and skip members.
            return this;
        }

        class MemberGetter : ExpressionVisitor
        {
            private MemberExpression? _node;

            protected override Expression VisitMember(MemberExpression node)
            {
                return _node ??= node;
            }

            public MemberExpression? MemberExpression => _node;
        }
    }
}