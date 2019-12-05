using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRequestService _requestService;

        public EmployeeService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public void GetById()
        {
            _requestService.GetAsync<string>("https:ma/a/a");
        }
    }
    public interface IEmployeeService {
        void GetById();
    }
}
