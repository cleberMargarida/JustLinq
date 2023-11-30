using System;

namespace JustLinq.SqlServer
{
    internal class SqlServerExecuteQueryFactory : ExecuteQueryFactory
    {
        private readonly string connectionString;

        public SqlServerExecuteQueryFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override Func<IExecuteQuery> Create => 
            () => new SqlServerExecuteQuery(this.connectionString);
    }
}