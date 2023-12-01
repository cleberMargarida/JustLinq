namespace JustLinq.SqlServer
{
    internal class SqlServerExecuteQuery : IExecuteQuery
    {
        public SqlServerExecuteQuery(string connectionString)
        {
        }

        public TResult GetResult<TResult>(string query)
        {
            return default;
        }
    }
}