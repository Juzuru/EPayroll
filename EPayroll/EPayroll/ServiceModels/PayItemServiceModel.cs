using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.ServiceModels
{
    public class PayItemServiceModel
    {
        public float Amount { get; set; }
        public int TotalHour { get; set; }
        public float HourRate { get; set; }
        public string PayTypeName { get; set; }
    }

    public class GroupPayItemServiceModel
    {
        public string PayTypeCategoryName { get; set; }
        public IList<PayItemServiceModel> PayItems { get; set; }
    }
}
