using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string UserUID { get; set; }
    }
}
