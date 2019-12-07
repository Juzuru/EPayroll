using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.ServiceModels
{
    public class EmployeeCheckUserModel
    {
        public string Email { get; set; }
        public string UserUID { get; set; }
    }

    public class EmployeeAuthorizedModel
    {
        public string Token { get; set; }
        public string TokenType { get; set; }
        public Guid? Id { get; set; }
    }
}
