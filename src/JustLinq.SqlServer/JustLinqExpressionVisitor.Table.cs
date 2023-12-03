using System;
using System.Linq.Expressions;

namespace JustLinq.SqlServer
{
    internal partial class JustLinqTableExpressionVisitor : ExpressionVisitor
    {
        private TableExpression? _table;
       
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (!(_table is null))
            {
                return node;
            }

            if (node.Value is Table)
            {
                return VisitConstantTable((TableExpression)node);
            }

            return node;
        }

        protected Expression VisitConstantTable(TableExpression node)
        {
            return _table ??= node;
        }

        internal TableExpression? VisitTable(MethodCallExpression node)
        {
            base.VisitMethodCall(node);
            return _table;
        }
    }
}
