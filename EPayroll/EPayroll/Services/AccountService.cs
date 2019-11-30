using EPayroll.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Services
{
    public class AccountService : IAccountService
    {
        private static string base_uri = "http://45.119.83.107:9003/api/Accounts";

        protected IRequestService _requestService;

        public AccountService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public AccountTokenModel CheckUser(string name, string email, string picture)
        {
            return _requestService.PostAsync<AccountTokenModel>(base_uri + "/login", new AccountLoginModel { 
                Name = name,
                Email = email,
                Picture = picture
            });
        }
    }

    public interface IAccountService
    {
        AccountTokenModel CheckUser(string name, string email, string picture);
    }
}
