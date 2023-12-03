using System;
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

        public ColumnBuilder<T> HasName(string columnName)
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