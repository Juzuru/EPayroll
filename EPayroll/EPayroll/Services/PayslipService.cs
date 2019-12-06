using EPayroll.Models;
using EPayroll.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPayroll.Services
{
    public class PayslipService : IPayslipService
    {
        private readonly string base_uri = "http://45.119.83.107:9003/api/PaySlips";

        private readonly IRequestService _requestService;

        public PayslipService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<IList<Payslip>> GetAllAsync(Guid employeeId)
        {
            IList<Payslip> result = new List<Payslip>();

            var list = await _requestService.GetAsync<IList<PayslipServiceModel>>(base_uri + "?employeeId=" + employeeId.ToString());
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    result.Add(new Payslip { 
                        Amount = list[i].Amount,
                        Id = list[i].Id,
                        OrdinalNumber = i + 1,
                        Status = list[i].Status,
                        PaySlipCode = list[i].PaySlipCode
                    });
                }

            }

            return result;
        }
    }

    public interface IPayslipService
    {
        Task<IList<Payslip>> GetAllAsync(Guid employeeId);
    }
}
