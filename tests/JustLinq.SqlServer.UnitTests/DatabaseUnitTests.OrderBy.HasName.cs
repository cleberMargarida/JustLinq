namespace JustLinq.SqlServer.UnitTests
{
    public partial class DatabaseUnitTests
    {
        [Fact]
        public void CreateQuery_HasName_OrderBy()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[employee_id], [Employee].[employee_name], [Employee].[employee_email], [Employee].[employee_phone], [Employee].[employee_startdate]
            FROM [employees] AS [Employee]
            ORDER BY [Employee].[employee_name]
            """;

            //act
            var query = database.Query<Employee>(table => 
                                {
                                    table.Name("employees");
                                    table.Column(c => c.Id).Name("employee_id");
                                    table.Column(c => c.Name).Name("employee_name");
                                    table.Column(c => c.Phone).Name("employee_phone");
                                    table.Column(c => c.Email).Name("employee_email");
                                    table.Column(c => c.StartDate).Name("employee_startdate");
                                })
                                .OrderBy(x => x.Name)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }
        

        [Fact]
        public void CreateQuery_HasName_OrderBy_Select()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[employee_id]
            FROM [employees] AS [Employee]
            ORDER BY [Employee].[employee_name]
            """;

            //act
            var query = database.Query<Employee>(table =>
                                {
                                    table.Name("employees");
                                    table.Column(c => c.Id).Name("employee_id");
                                    table.Column(c => c.Name).Name("employee_name");
                                    table.Column(c => c.Phone).Name("employee_phone");
                                    table.Column(c => c.Email).Name("employee_email");
                                    table.Column(c => c.StartDate).Name("employee_startdate");
                                })
                                .OrderBy(x => x.Name)
                                .Select(x => x.Id)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_HasName_OrderBy_Where()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT [Employee].[employee_id], [Employee].[employee_name], [Employee].[employee_email], [Employee].[employee_phone], [Employee].[employee_startdate]
            FROM [employees] AS [Employee]
            WHERE [Employee].[employee_id] > 0
            ORDER BY [Employee].[employee_name]
            """;

            //act
            var query = database.Query<Employee>(table =>
                                {
                                    table.Name("employees");
                                    table.Column(c => c.Id).Name("employee_id");
                                    table.Column(c => c.Name).Name("employee_name");
                                    table.Column(c => c.Phone).Name("employee_phone");
                                    table.Column(c => c.Email).Name("employee_email");
                                    table.Column(c => c.StartDate).Name("employee_startdate");
                                })
                                .OrderBy(x => x.Name)
                                .Where(x => x.Id > 0)
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }

        [Fact]
        public void CreateQuery_HasName_OrderBy_FirstOrDefault()
        {
            //arrange
            const string ExpectedQuery =
            """
            SELECT TOP(1) [Employee].[employee_id], [Employee].[employee_name], [Employee].[employee_email], [Employee].[employee_phone], [Employee].[employee_startdate]
            FROM [employees] AS [Employee]
            ORDER BY [Employee].[employee_name]
            """;

            //act
            var query = database.Query<Employee>(table =>
                                {
                                    table.Name("employees");
                                    table.Column(c => c.Id).Name("employee_id");
                                    table.Column(c => c.Name).Name("employee_name");
                                    table.Column(c => c.Phone).Name("employee_phone");
                                    table.Column(c => c.Email).Name("employee_email");
                                    table.Column(c => c.StartDate).Name("employee_startdate");
                                })
                                .OrderBy(x => x.Name)
                                .FirstOrDefault()
                                .ToQueryString();

            //assert
            query.Should().Be(ExpectedQuery);
        }
    }
}