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
        public long Amount { get; set; }
        public PayPeriodServiceModel PayPeriod { get; set; }
    }

    public class PayslipDetailServiceModel
    {
        public long Amount { get; set; }
        public PayPeriodServiceModel PayPeriod { get; set; }
        public IList<GroupPayItemServiceModel> GroupPayItems { get; set; }
    }

    public class PayslipConfirmServiceModel
    {
        public Guid Id { get; set; }
        public bool Accepted { get; set; }
    }
}
