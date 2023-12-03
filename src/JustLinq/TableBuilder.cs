using System.Collections.Generic;

namespace JustLinq
{
    public class TableBuilder<T>
    {
        private string? tableName;

        public TableBuilder<T> Name(string tableName)
        {
            this.tableName = tableName;
            return this;
        }

        public Table<T> Build() => new Table<T>
        {
            TableName = tableName ?? typeof(T).Name,
        };
    }
}