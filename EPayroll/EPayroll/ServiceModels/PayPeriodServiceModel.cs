using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.ServiceModels
{
    public class PayPeriodServiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PayDate { get; set; }
    }
}
