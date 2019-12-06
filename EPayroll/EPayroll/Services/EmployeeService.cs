
﻿using EPayroll.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace EPayroll.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string base_uri = "http://45.119.83.107:9003/api/Employees";

        private readonly IRequestService _requestService;

        public EmployeeService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public async Task<Guid?> CheckUserAsync(string email, string userUID)
        {
            try
            {
                EmployeeAuthorizedModel model = await _requestService
                .PostAsync<EmployeeAuthorizedModel>(base_uri + "/check-user", new EmployeeCheckUserModel
                {
                    Email = email,
                    UserUID = userUID
                });

                return model.Id;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("404"))
                {
                    return null;
                }
                throw e;
            }
        }
    }

    public interface IEmployeeService
    {
        Task<Guid?> CheckUserAsync(string email, string userUID);
    }
}
