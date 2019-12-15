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

    public class EmployeeDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string IdentifyNumber { get; set; }
        public PositionServiceViewModel Position { get; set; }
        public SalaryModeServiceViewModel SalaryMode { get; set; }
        public SalaryLevelServiceViewModel SalaryLevel { get; set; }
    }
    public class EmployeeServiceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PositionServiceModel Position { get; set; }
    }

    public class EmployeeSendFCMTokenModel
    {
        public string FCMToken { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
