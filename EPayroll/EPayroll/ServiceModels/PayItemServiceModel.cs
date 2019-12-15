using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.ServiceModels
{
    public class PayItemServiceModel
    {
        public long Amount { get; set; }
        public int TotalHour { get; set; }
        public long HourRate { get; set; }
        public string PayTypeName { get; set; }
    }

    public class GroupPayItemServiceModel
    {
        public string PayTypeCategoryName { get; set; }
        public IList<PayItemServiceModel> PayItems { get; set; }
    }
}
