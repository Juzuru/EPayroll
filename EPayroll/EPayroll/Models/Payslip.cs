using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.Models
{
    public class PaySlipModel
    {
        public Guid Id { get; set; }
        public string PaySlipCode { get; set; }
        public DateTime CreatedDate { get; set; }

        public PayPeriodModel PayPeriod { get; set; }
    }

    public class PaySlipCreateModel
    {
        public string PaySlipCode { get; set; }

        public Guid PayPeriodId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
