
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.DTO
{
    public class EmployeesDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string? Email {get; set; }
        public string Position { get; set; } = null!;
    }
}