using System;
using System.Linq;

namespace JustLinq
{
    public interface IDatabase
    {
        IQueryable<TEntity> Query<TEntity>(Action<TableBuilder<TEntity>>? decorate = null)
            where TEntity : new();
    }
}
