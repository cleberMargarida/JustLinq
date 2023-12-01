using System;
using System.Linq;

namespace JustLinq
{
    public interface IDatabase
    {
        IQueryable<TEntity> CreateQuery<TEntity>(Action<TableBuilder<TEntity>>? decorate = null);
        IQueryable<TEntity> CreateQuery<TEntity>(string table, Action<TableBuilder<TEntity>>? decorate = null);
    }
}
