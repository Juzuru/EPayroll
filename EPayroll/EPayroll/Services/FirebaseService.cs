using EPayroll.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly string uri = "http://45.119.83.107:9003/api/Employees/send-fcm-token";

        private readonly IRequestService _requestService;

        public FirebaseService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public bool SendFCMToken(Guid employeeId, string token)
        {
            try
            {
                _requestService.Post<string>(uri, new EmployeeSendFCMTokenModel
                {
                    EmployeeId = employeeId,
                    FCMToken = token
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public interface IFirebaseService
    {
        bool SendFCMToken(Guid employeeId, string token);
    }
}
