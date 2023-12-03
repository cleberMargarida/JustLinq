namespace JustLinq.SqlServer.UnitTests
{
    public partial class DatabaseUnitTests
    {
        [Fact]
        public void CreateQuery_Where()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id], [Employee].[Name], [Employee].[Email], [Employee].[Phone], [Employee].[StartDate]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Where(x => x.Name == "Bob Smith")
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }



        [Fact]
        public void CreateQuery_WhereWithAnd()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
              AND [Employee].[Email] = 'bob.smith@example.com'
              AND [Employee].[Id] > 0
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Where(x => x.Name == "Bob Smith" &&
                                            x.Email == "bob.smith@example.com" &&
                                            x.Id > 0)
                                .Select(x => x.Id)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_WhereWithOr()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id], [Employee].[Name], [Employee].[Email], [Employee].[Phone], [Employee].[StartDate]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
              || [Employee].[Email] = 'bob.smith@example.com'
              || [Employee].[Id] > 0
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Where(x => x.Name == "Bob Smith" ||
                                            x.Email == "bob.smith@example.com" ||
                                            x.Id > 0)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_Where_Select()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Where(x => x.Name == "Bob Smith")
                                .Select(x => x.Id)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_WhereWithAnd_Select()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
              AND [Employee].[Email] = 'bob.smith@example.com'
              AND [Employee].[Id] > 0
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Where(x => x.Name == "Bob Smith" &&
                                            x.Email == "bob.smith@example.com" &&
                                            x.Id > 0)
                                .Select(x => x.Id)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_WhereWithOr_Select()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
              || [Employee].[Email] = 'bob.smith@example.com'
              || [Employee].[Id] > 0
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Where(x => x.Name == "Bob Smith" ||
                                            x.Email == "bob.smith@example.com" ||
                                            x.Id > 0)
                                .Select(x => x.Id)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_Where_OrderBy()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[Id], [Employee].[Name], [Employee].[Email], [Employee].[Phone], [Employee].[StartDate]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
            ORDER BY [Employee].[Id]
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Where(x => x.Name == "Bob Smith")
                                .OrderBy(x => x.Id)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_Where_FirstOrDefault()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT TOP(1) [Employee].[Id], [Employee].[Name], [Employee].[Email], [Employee].[Phone], [Employee].[StartDate]
            FROM [Employee] AS [Employee]
            WHERE [Employee].[Name] = 'Bob Smith'
            """;

            //act
            var query = database.CreateQuery<Employee>()
                                .Where(x => x.Name == "Bob Smith")
                                .FirstOrDefault()
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }
    }
}
