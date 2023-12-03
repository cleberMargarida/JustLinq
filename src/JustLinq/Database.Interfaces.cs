using System;
using System.Linq;

namespace JustLinq
{
    public interface IDatabase
    {
        IQueryable<TEntity> CreateQuery<TEntity>(Action<TableBuilder<TEntity>>? decorate = null)
            where TEntity : new();
    }
}
