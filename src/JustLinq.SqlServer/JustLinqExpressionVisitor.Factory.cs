using JustLinq.SqlServer;
using System.Linq;

namespace JustLinq.SqlServer
{
    internal class JustLinqExpressionVisitorFactory : IExpressionPrinterFactory
    {
        private static readonly JustLinqExpressionVisitorFactory _instance = new JustLinqExpressionVisitorFactory();

        private JustLinqExpressionVisitorFactory() { }

        public static JustLinqExpressionVisitorFactory FactorySingleton => _instance;

        internal JustLinqExpressionVisitor Create(string? methodName = "") => methodName switch
        {
            nameof(Queryable.Join) => JoinVisitor,
            _ => DefaultVisitor
        };

        internal JustLinqJoinExpressionVisitor JoinVisitor => new JustLinqJoinExpressionVisitor();
        internal JustLinqTableExpressionVisitor TableVisitor => new JustLinqTableExpressionVisitor();
        internal JustLinqExpressionVisitor DefaultVisitor => new JustLinqExpressionVisitor();

        IExpressionPrinter IExpressionPrinterFactory.Create(string? methodName) => Create(methodName);
    }
}
