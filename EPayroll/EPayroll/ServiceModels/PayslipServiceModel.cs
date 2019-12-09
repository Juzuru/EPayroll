using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.ServiceModels
{
    public class PayslipServiceModel
    {
        public Guid Id { get; set; }
        public string PaySlipCode { get; set; }
        public string Status { get; set; }
        public float Amount { get; set; }
    }

    public class PayslipDetailServiceModel
    {
        public float Amount { get; set; }
        public PayPeriodServiceModel PayPeriod { get; set; }
        public IList<GroupPayItemServiceModel> GroupPayItems { get; set; }
    }
}
