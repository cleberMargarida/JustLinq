using System;

namespace JustLinq
{
    internal abstract class ExecuteQueryFactory : IExecuteQueryFactory
    {
        public abstract Func<IExecuteQuery> Create { get; }
    }
}