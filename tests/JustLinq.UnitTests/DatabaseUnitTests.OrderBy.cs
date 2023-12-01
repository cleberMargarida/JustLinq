namespace JustLinq.UnitTests
{
    public partial class DatabaseUnitTests
    {

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