using EPayroll.Models;
using EPayroll.ServiceModels;
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

        public Guid? CheckUser(string email, string userUID)
        {
            try
            {
                EmployeeAuthorizedModel model = _requestService
                    .Post<EmployeeAuthorizedModel>(base_uri + "/check-user", new EmployeeCheckUserModel
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

        public EmployeeDetailModel GetDetail(Guid id)
        {
            EmployeeDetailModel employeeDetailModel;
            try
            {
                employeeDetailModel = _requestService
                    .Get<EmployeeDetailModel>(base_uri + "/" + id.ToString());

            }
            catch (Exception e)
            {
                if (e.Message.Contains("404"))
                {
                    return null;
                }
                throw e;
            }
            return employeeDetailModel;
        }

        public Employee GetById(Guid employeeId)
        {
            EmployeeServiceViewModel model = _requestService.Get<EmployeeServiceViewModel>(base_uri + "/" + employeeId.ToString());
            return new Employee
            {
                Id = model.Id,
                Name = model.Name,
                Position = model.Position.Name
            };
        }
    }

    public interface IEmployeeService
    {
        Guid? CheckUser(string email, string userUID);
        EmployeeDetailModel GetDetail(Guid id);
        Employee GetById(Guid employeeId);
        Guid? CheckUser(string email, string userUID);
    }
}
