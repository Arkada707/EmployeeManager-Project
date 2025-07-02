using Microsoft.EntityFrameworkCore;
using EmployeeManager.Models;

namespace EmployeeManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Employee> Employees { get; set; }

}