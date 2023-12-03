namespace JustLinq.UnitTests
{
    public partial class DatabaseUnitTests
    {
        [Fact]
        public void CreateQuery_OrderBy()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id], [Employee].[Name], [Employee].[Email], [Employee].[Phone], [Employee].[StartDate]
            FROM [Employee] AS [Employee]
            ORDER BY [Employee].[Name]
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .OrderBy(x => x.Name)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }
        

        [Fact]
        public void CreateQuery_OrderBy_Select()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id]
            FROM [Employee] AS [Employee]
            ORDER BY [Employee].[Name]
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .OrderBy(x => x.Name)
                                .Select(x => x.Id)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_OrderBy_Where()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id], [Employee].[Name], [Employee].[Email], [Employee].[Phone], [Employee].[StartDate]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Id] > 0
            ORDER BY [Employee].[Name]
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .OrderBy(x => x.Name)
                                .Where(x => x.Id > 0)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_OrderBy_FirstOrDefault()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT TOP(1) [Employee].[Id], [Employee].[Name], [Employee].[Email], [Employee].[Phone], [Employee].[StartDate]
            FROM [Employee] AS [Employee]
            ORDER BY [Employee].[Name]
            """;
            
            //act
            var query = database.CreateQuery<Employee>()
                                .OrderBy(x => x.Name)
                                .FirstOrDefault()
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }
    }
}