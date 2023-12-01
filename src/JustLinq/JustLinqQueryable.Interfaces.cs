using System.Linq;

namespace JustLinq
{
    public interface IJustLinqQueryable : IQueryable
    {
    }

    public interface IJustLinqQueryable<out T> : IQueryable<T>
    {
    }
    
    public interface IJustLinqOrderByQueryable<out T> : IJustLinqQueryable<T>, IOrderedQueryable<T>
    {
    }

}
