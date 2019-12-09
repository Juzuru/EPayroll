using EPayroll.Models;
using EPayroll.ServiceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

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

        public ObservableCollection<Payslip> GetAll(Guid employeeId)
        {
            ObservableCollection<Payslip> result = new ObservableCollection<Payslip>();

            var list = _requestService
                .Get<IList<PayslipServiceModel>>(base_uri + "?employeeId=" + employeeId.ToString());
            string status;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    status = list[i].Status;
                    if (status.Equals("Paid")) status = "Đã trả";
                    else if (status.Equals("Unpaid")) status = "Chưa trả";
                    else status = "Đang xử lí";

                    result.Add(new Payslip { 
                        Amount = list[i].Amount,
                        Id = list[i].Id,
                        OrdinalNumber = i + 1,
                        Status = status,
                        PaySlipCode = list[i].PaySlipCode
                    });
                }

            }

            return result;
        }

        public PayslipDetail GetDetail(Guid payslipId)
        {
            PayslipDetailServiceModel model = _requestService
                .Get<PayslipDetailServiceModel>(base_uri + "/" + payslipId.ToString());

            ObservableCollection<PayItem> payItems = new ObservableCollection<PayItem>();
            PayItemServiceModel payItemServiceModel;
            for (int i = 0; i < model.GroupPayItems.Count; i++)
            {
                payItems.Add(new PayItem
                {
                    PayTypeName = model.GroupPayItems[i].PayTypeCategoryName,
                    OrdinalNumber = -1
                });
                for (int j = 0; j < model.GroupPayItems[i].PayItems.Count; j++)
                {
                    payItemServiceModel = model.GroupPayItems[i].PayItems[j];
                    payItems.Add(new PayItem
                    {
                        OrdinalNumber = j + 1,
                        Amount = payItemServiceModel.Amount,
                        HourRate = payItemServiceModel.HourRate,
                        PayTypeName = payItemServiceModel.PayTypeName,
                        TotalHour = payItemServiceModel.TotalHour
                    });
                }
            }

            return new PayslipDetail 
            { 
                PayPeriod = new PayPeriod
                {
                    Name = model.PayPeriod.Name,
                    PayDate = model.PayPeriod.PayDate.ToString("dd/MM/yyyy"),
                    Period = model.PayPeriod.EndDate.ToString("dd/MM/yyyy") + " - " + model.PayPeriod.StartDate.ToString("dd/MM/yyyy")
                },
                PayItems = payItems,
                Amount = model.Amount
            };
        }
    }

    public interface IPayslipService
    {
        ObservableCollection<Payslip> GetAll(Guid employeeId);
        PayslipDetail GetDetail(Guid payslipId);
    }
}
