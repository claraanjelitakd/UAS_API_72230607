using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);

    }
}