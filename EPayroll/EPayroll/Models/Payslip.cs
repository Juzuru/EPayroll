using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EPayroll.Models
{
    public class Payslip
    {
        public Guid Id { get; set; }
        public string PaySlipCode { get; set; }
        public int OrdinalNumber { get; set; }
        public float Amount { get; set; }
        public string Status { get; set; }
    }

    public class PayslipDetail
    {
        public float Amount { get; set; }
        public PayPeriod PayPeriod { get; set; }
        public ObservableCollection<PayItem> PayItems { get; set; }
    }
}
