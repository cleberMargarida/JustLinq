namespace JustLinq
{
    internal abstract class ExecuteQuery : IExecuteQuery
    {
        public abstract TResult GetResult<TResult>(string query);
    }
}