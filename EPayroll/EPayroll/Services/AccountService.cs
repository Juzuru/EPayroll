using EPayroll.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Services
{
    public class AccountService : IAccountService
    {
        private static string base_uri = "http://192.168.1.15:50718/api/Account";

        protected IRequestService _requestService;

        public AccountService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public AccountViewModel CheckLogin(string employeeId, string password)
        {
            return _requestService.PostAsync<AccountViewModel>(base_uri + "/login", new AccountLoginModel { 
                EmployeeId = employeeId,
                Password = password
            });
        }
    }

    public interface IAccountService
    {
        AccountViewModel CheckLogin(string employeeId, string password);
    }
}
