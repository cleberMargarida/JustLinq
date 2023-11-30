using System;

namespace JustLinq
{
    public interface IExecuteQuery
    {
        TResult GetResult<TResult>(string query);
    }

    public interface IExecuteQueryFactory
    {
        Func<IExecuteQuery> Create { get; }
    }
}