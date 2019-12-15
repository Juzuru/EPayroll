using EPayroll.Models;
using EPayroll.Resources;
using EPayroll.Services;
using EPayroll.ViewModels.Bases;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
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
        private Employee _employee;
        private PayPeriod _payPeriod;
        private Payslip _payslip;
        private ObservableCollection<PayItem> _payItems;
        private bool _isAccept;
        private bool _isUnaccept;
        private bool _isWaiting;
        #endregion

        #region Binding Values
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
        public Payslip Payslip
        {
            get => _payslip;
            set => SetValue(ref _payslip, value);
        }
        public ObservableCollection<PayItem> PayItems
        {
            get => _payItems;
            set => SetValue(ref _payItems, value);
        }
        public bool IsAccept
        {
            get => _isAccept;
            set => SetValue(ref _isAccept, value);
        }
        public bool IsUnaccept
        {
            get => _isUnaccept;
            set => SetValue(ref _isUnaccept, value);
        }
        public bool IsWaiting
        {
            get => _isWaiting;
            set => SetValue(ref _isWaiting, value);
        }
        #endregion

        #region Command
        public Command UnacceptCommand
        {
            get => new Command(() =>
            {
                IsAccept = false;
                IsUnaccept = true;
                IsWaiting = false;
                _isConfirmed = true;
                _payslipService.Confirm(Payslip.Id, false);
            });
        }
        public Command AcceptCommand
        {
            get => new Command(() =>
            {
                IsAccept = true;
                IsUnaccept = false;
                IsWaiting = false;
                _isConfirmed = true;
                _payslipService.Confirm(Payslip.Id, true);
            });
        }
        #endregion

        private bool _isConfirmed = false;

        public override void OnAppearing()
        {
            IsLoading = true;

            Employee = AppResource.Get<Employee>(ParameterName.Employee);
            PayslipDetail payslipDetail;
            Payslip = AppResource.Get<Payslip>(ParameterName.PayslipId);
            if (AppResource.Get(Payslip.Id.ToString(), out PayslipDetail value))
            {
                payslipDetail = value;
            }
            else
            {
                payslipDetail = _payslipService.GetDetail(Payslip.Id);
                AppResource.Add(Payslip.Id.ToString(), payslipDetail);
            }
            PayItems = payslipDetail.PayItems;
            PayPeriod = payslipDetail.PayPeriod;

            if (Payslip.Status.Equals("Chấp nhận"))
            {
                IsAccept = true;
                IsUnaccept = false;
                IsWaiting = false;
            }
            else if (Payslip.Status.Equals("Từ chối"))
            {
                IsAccept = false;
                IsUnaccept = true;
                IsWaiting = false;
            }
            else
            {
                IsAccept = false;
                IsUnaccept = false;
                IsWaiting = true;
            }

            IsLoading = false;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            if (_isConfirmed)
            {
                parameters.Add(ParameterName.Accepted, IsAccept);
                parameters.Add(ParameterName.PayslipId, Payslip.Id);
            }
        }
    }
}
