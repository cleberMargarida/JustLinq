using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JustLinq.UnitTests")]
[assembly: InternalsVisibleTo("JustLinq.SqlServer")]

namespace JustLinq
{
    internal class Database : IDatabase
    {
        private readonly IExecuteQuery executeQuery;

        public Database(Action<DatabaseOptions> apply)
        {
            var options = new DatabaseOptions();
            apply(options);
            executeQuery = options.ExecuteQuery;
        } 

        public IQueryable<TEntity> CreateQuery<TEntity>(Action<TableBuilder<TEntity>>? decorate = default)
        {
            var table = BuildTableMap(decorate);
            return CreateQuery(table);
        }

        private IQueryable<TEntity> CreateQuery<TEntity>(Table<TEntity> table)
        {
            var provider = new JustLinqQueryProvider(this.executeQuery);
            var expression = Expression.Constant(table);
            return new JustLinqQueryable<TEntity>(provider, expression);
        }

        private static Table<TEntity> BuildTableMap<TEntity>(Action<TableBuilder<TEntity>>? decorate)
        {
            var tableMapBuilder = new TableBuilder<TEntity>();
            decorate?.Invoke(tableMapBuilder);
            var table = tableMapBuilder.Build();
            return table;
        }

    }
}
