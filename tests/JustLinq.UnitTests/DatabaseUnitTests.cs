using FluentAssertions;

namespace JustLinq.UnitTests;

public class DatabaseUnitTests
{
    private readonly IDatabase database;

    public DatabaseUnitTests(IDatabase database)
    {
        this.database = database;
    }

    [Fact]
    public void CreateQuery_Where_Select()
    {
        // Arrange
        const string ExpectedQuery =
        """
        
            SELECT
                Employee.Id 
            FROM
                Employee AS Employee 
            WHERE
                Employee.Name = "Cleber"
        """;

        // Act
        var query = database.CreateQuery<Employee>()
                            .Where(x => x.Name == "Cleber")
                            .Select(x => x.Id)
                            .ToQueryString();


        //Assert
        query.Should().Be(ExpectedQuery);
    }

    [Fact]
    public void CreateQuery_Select_Where()
    {
        // Arrange
        const string ExpectedQuery =
        """
        
            SELECT
                Employee.Name 
            FROM
                Employee AS Employee 
            WHERE
                Employee.Name = "Cleber"
        """;

        // Act
        var query = database.CreateQuery<Employee>()
                            .Select(x => x.Name)
                            .Where(x => x == "Cleber")
                            .ToQueryString();


        //Assert
        query.Should().Be(ExpectedQuery);
    }
}
