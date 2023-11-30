using System.Linq.Expressions;

namespace JustLinq
{
    public interface IExpressionTranslator
    {
        string Translate(Expression expression);
    }
}
