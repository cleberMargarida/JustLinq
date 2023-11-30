using JustLinq.SqlServer;

namespace JustLinq.SqlServer
{
    public interface IExpressionPrinterFactory
    {
        IExpressionPrinter Create(string? methodName = "");
    }
}
