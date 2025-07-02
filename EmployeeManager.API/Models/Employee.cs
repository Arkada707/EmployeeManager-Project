namespace EmployeeManager.Models;

public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Position { get; set; }
    public required decimal Salary { get; set; }
}