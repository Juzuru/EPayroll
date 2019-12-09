using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EPayroll.Models
{
    public class PayItem
    {
        public int OrdinalNumber { get; set; }
        public float? Amount { get; set; }
        public int? TotalHour { get; set; }
        public float? HourRate { get; set; }
        public string PayTypeName { get; set; }
    }
}
