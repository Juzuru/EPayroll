using System;
using System.Collections.Generic;
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
}
