using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Models;
using EmployeeManager.Data;

namespace EmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Employees.ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var emp = _context.Employees.Find(id);
        return emp == null ? NotFound() : Ok(emp);
    }

    [HttpPost]
    public IActionResult Create(Employee emp)
    {
        _context.Employees.Add(emp);
        _context.SaveChanges();
        return Ok(emp);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Employee updated)
    {
        var emp = _context.Employees.Find(id);
        if (emp == null) return NotFound();

        emp.Name = updated.Name;
        emp.Position = updated.Position;
        emp.Salary = updated.Salary;
        _context.SaveChanges();

        return Ok(emp);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var emp = _context.Employees.Find(id);
        if (emp == null) return NotFound();

        _context.Employees.Remove(emp);
        _context.SaveChanges();

        return Ok();
    }
}
