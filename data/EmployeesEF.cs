using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class EmployeesEF:IEmployees
    {
        private readonly ApplicationDbContext _context;

    public EmployeesEF(ApplicationDbContext context)
    {
        _context = context;
    }

    public void DeleteEmployees(int EmployeeID)
    {
        var employee = _context.Employees.Find(EmployeeID);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }

        IEnumerable<Employees> IEmployees.GetEmployees()
        {
            return _context.Employees.ToList();
        }

        Employees IEmployees.GetEmployeesById(int EmployeeID)
        {
            return _context.Employees.FirstOrDefault(e => e.EmployeeId == EmployeeID);
        }

        public Employees AddEmployees(Employees employees)
        {
            _context.Employees.Add(employees);
        _context.SaveChanges();
        return employees;
        }

        public Employees UpdateEmployees(Employees employees)
        {
            var existing = _context.Employees.Find(employees.EmployeeId);
        if (existing == null)
            return null;

        existing.EmployeeName = employees.EmployeeName;
        existing.Position = employees.Position;
        existing.Email = employees.Email;
        // tambah properti lain jika ada

        _context.SaveChanges();
        return existing;
        }
    }
}