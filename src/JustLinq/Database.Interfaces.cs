using System;
using System.Linq;

namespace JustLinq
{
    public interface IDatabase
    {
        IJustLinqQueryable<TEntity> CreateQuery<TEntity>(Action<TableBuilder<TEntity>>? decorate = null);
        IJustLinqQueryable<TEntity> CreateQuery<TEntity>(string table, Action<TableBuilder<TEntity>>? decorate = null);
    }
}
