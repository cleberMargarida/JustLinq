namespace JustLinq.UnitTests
{
    public partial class DatabaseUnitTests
    {

        [Fact]
        public void CreateQuery_Select()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Name]
            FROM [Employee] AS [Employee]
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Select(x => x.Name)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_Select_Where()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Name]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Select(x => x.Name)
                                .Where(x => x == "Bob Smith")
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_Select_OrderBy()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Name]
            FROM [Employee] AS [Employee]
            ORDER BY [Employee].[Name]
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Select(x => x.Name)
                                .OrderBy(x => x)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }
        [Fact]
        public void CreateQuery_Select_FirstOrDefault()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT TOP(1) [Employee].[Name]
            FROM [Employee] AS [Employee]
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Select(x => x.Name)
                                .FirstOrDefault()
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }
    }
}
