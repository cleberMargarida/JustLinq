namespace JustLinq.UnitTests
{
    public partial class DatabaseUnitTests
    {

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
    }
}
