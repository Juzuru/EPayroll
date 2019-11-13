using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Services
{
    public class AccountService : IAccountService
    {
        public bool CheckLogin(string employeeId, string password)
        {
            return employeeId.Equals("123");
        }
    }

    public interface IAccountService
    {
        bool CheckLogin(string employeeId, string password);
    }
}
