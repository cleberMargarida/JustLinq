namespace JustLinq.SqlServer
{
    public static class DatabaseExtensions
    {
        public static void UseSqlServer(this DatabaseOptions options, string connectionString)
        {
            var factory = CreateExecuteQueryFactory(connectionString);
            options.ExecuteQuery = factory.Create();

            ExpressionTranslator.Shared = CreateExpressionTranslator();
        }

        private static SqlServerExecuteQueryFactory CreateExecuteQueryFactory(string connectionString)
        {
            return new SqlServerExecuteQueryFactory(connectionString);
        }

        private static SqlServerExpressionTranslator CreateExpressionTranslator()
        {
            return new SqlServerExpressionTranslator();
        }
    }
}
