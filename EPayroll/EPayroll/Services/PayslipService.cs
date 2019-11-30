using EPayroll.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IList<string> GetAll()
        {
            var listPayslip = _requestService.GetAsync<IList<PaySlipModel>>(base_uri);
            IList<string> result = new List<string>();
            for (int i = 0; i < listPayslip.Count; i++)
            {
                result.Add(listPayslip[i].PaySlipCode);
            }

            return result;
        }
    }

    public interface IPayslipService
    {
        IList<string> GetAll();
    }
}
