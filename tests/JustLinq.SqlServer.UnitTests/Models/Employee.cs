namespace JustLinq.SqlServer.UnitTests.Models;
#pragma warning disable CS8618

internal class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Phone { get; set; }
    public DateTime StartDate { get; set; }
}