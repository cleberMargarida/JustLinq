using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JustLinq.UnitTests")]
[assembly: InternalsVisibleTo("JustLinq.SqlServer")]

namespace JustLinq
{
    internal class Database : IDatabase
    {
        private readonly IExecuteQuery executeQuery;

        public Database(Action<DatabaseOptions> options)
        {
            var optionsInstance = new DatabaseOptions();
            options(optionsInstance);
            executeQuery = optionsInstance.ExecuteQuery;
        } 

        public IJustLinqQueryable<TEntity> CreateQuery<TEntity>(Action<TableBuilder<TEntity>>? decorate = default)
        {
            var table = BuildTableMap(decorate);
            table.TableName = typeof(TEntity).Name;
            return CreateQuery(table);
        }

        public IJustLinqQueryable<TEntity> CreateQuery<TEntity>(string tableName, Action<TableBuilder<TEntity>>? decorate = default)
        {
            var table = BuildTableMap(decorate);
            table.TableName = tableName;
            return CreateQuery(table);
        }

        private IJustLinqQueryable<TEntity> CreateQuery<TEntity>(Table<TEntity> table)
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
