using EPayroll.Models;
using EPayroll.Resources;
using EPayroll.Services;
using EPayroll.ViewModels.Bases;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPayroll.ViewModels
{
    public class PayslipDetailViewModel : ViewModelBase
    {
        private readonly IPayslipService _payslipService;

        public PayslipDetailViewModel(IPayslipService payslipService, INavigationService navigationService) : base(navigationService)
        {
            _payslipService = payslipService;
        }

        #region Previous Values
        private bool _isLoading = true;
        private Employee _employee;
        private PayPeriod _payPeriod;
        private float _amount;
        private ObservableCollection<PayItem> _payItems;
        #endregion

        #region Binding Values
        public bool IsLoading
        {
            get => _isLoading;
            set => SetValue(ref _isLoading, value);
        }
        public Employee Employee
        {
            get => _employee;
            set => SetValue(ref _employee, value);
        }
        public PayPeriod PayPeriod
        {
            get => _payPeriod;
            set => SetValue(ref _payPeriod, value);
        }
        public float Amount
        {
            get => _amount;
            set => SetValue(ref _amount, value);
        }
        public ObservableCollection<PayItem> PayItems
        {
            get => _payItems;
            set => SetValue(ref _payItems, value);
        }
        #endregion

        private PayslipDetail payslipDetail;
        public override void OnAppearing()
        {
            Employee = AppResource.Get<Employee>(ParameterName.Employee);

            Guid payslipId = AppResource.Get<Guid>(ParameterName.PayslipId);
            if (AppResource.Get(payslipId.ToString(), out PayslipDetail value))
            {
                Amount = value.Amount;
                PayPeriod = value.PayPeriod;
                PayItems = value.PayItems;
            }
            else
            {
                payslipDetail = _payslipService.GetDetail(payslipId);

                Amount = payslipDetail.Amount;
                PayItems = payslipDetail.PayItems;
                PayPeriod = payslipDetail.PayPeriod;
            }
        }

        public override void OnDisappearing()
        {
            AppResource.Add(ParameterName.PayslipId, payslipDetail);
        }
    }
}
