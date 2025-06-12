using System;
using SimpleRESTApi.Models;
namespace SimpleRESTApi.Data
{
    public interface IEmployees
    {
        // CRUD
        IEnumerable<Employees> GetEmployees();
        Employees GetEmployeesById(int EmployeeID);
        Employees AddEmployees(Employees employees);
        Employees UpdateEmployees(Employees employees);
        void DeleteEmployees(int EmployeeID);
    }
}