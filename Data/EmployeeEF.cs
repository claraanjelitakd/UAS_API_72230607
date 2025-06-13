using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public class EmployeeEF : IEmployee
    {
        private readonly ApplicationDbContext _context;
        public EmployeeEF(ApplicationDbContext context)
        {
            _context = context; 
        } 

        public void DeleteEmployee(int employeeId)
        {
            var employee = GetEmployeeById(employeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }
            try
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                throw new Exception("Error deleting employee: " + ex.Message);
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = from c in _context.Employees
                            orderby c.Name descending
                            select c;
            return employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            var employee = (from c in _context.Employees
                            where c.EmployeeID == employeeId
                            select c).FirstOrDefault();
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }
            return employee;
        }


        Employee IEmployee.AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                throw new Exception("Error adding employee: " + ex.Message);
            }
        }

        Employee IEmployee.UpdateEmployee(Employee employee)
        {
            var existingEmployee = GetEmployeeById(employee.EmployeeID);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }
            try
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Position = employee.Position;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                _context.Employees.Update(existingEmployee);
                _context.SaveChanges();
                return existingEmployee;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                throw new Exception("Error updating employee: " + ex.Message);
            }
        }
    }
}