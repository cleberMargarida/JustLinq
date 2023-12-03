using JustLinq.SqlServer;
using System.Linq.Expressions;
using static JustLinq.SqlServer.JustLinqExpressionVisitorFactory;

namespace JustLinq.SqlServer
{
    internal class JustLinqJoinExpressionVisitor : JustLinqExpressionVisitor
    {
        bool UseBase { get; set; } = true;

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (UseBase)
            {
                return base.VisitMethodCall(node);
            }
            _stringBuilder.Append('(');
            //TODO: implement join
            //_stringBuilder.Append(FactorySingleton.DefaultVisitor.Print(node));
            _stringBuilder.Append(')');
            _stringBuilder.Append(" AS ");

            var alias = Instance
                .TableVisitor
                .VisitTable(node)?
                .TableType.Name;

            _stringBuilder.Append(alias);
            SwitchUseBase();
            return node;
        }

        protected override Expression VisitJoin(JoinExpression node)
        {
            _stringBuilder.Append("SELECT ");
            Visit(node.Seletor);
            Visit(node.Left);
            _stringBuilder.Append(" INNER JOIN ");
            SwitchUseBase();
            Visit(node.Right);
            _stringBuilder.Append(" ON ");
            Visit(node.LeftKey);
            _stringBuilder.Append(" = ");
            Visit(node.RightKey);

            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            var columns = GetColumns(node.Type);
            _stringBuilder.Append(string.Join(", ", columns));
            return node;
        }

        protected override Expression VisitConstantTable(TableExpression node)
        {
            if (UseBase)
            {
                return base.VisitConstantTable(node);
            }

            _stringBuilder.Append(node.ToString());
            return node;
        }

        private void SwitchUseBase()
        {
            UseBase = !UseBase;
        }
    }
}