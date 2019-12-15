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
    public class ListPayslipViewModel : ViewModelBase
    {
        private readonly IPayslipService _payslipService;

        public ListPayslipViewModel(INavigationService navigationService, IPayslipService payslipService) : base(navigationService)
        {
            _payslipService = payslipService;
        }

        #region Previous Properties
        private ObservableCollection<Payslip> _payslips;
        #endregion

        #region Binding Properties
        public ObservableCollection<Payslip> Payslips
        {
            get => _payslips;
            set => SetValue(ref _payslips, value);
        }
        #endregion

        public Command TapPayslipCommand
        {
            get => new Command<int>(async (odinalNumber) =>
            {
                if (_isLoading) return;
                IsLoading = true;
                await Task.Run(() =>
                {
                    Opacity = 0;
                    while (Opacity <= 1)
                    {
                        Thread.Sleep(50);
                        Opacity += 0.1;
                    }
                });

                Payslip payslip = _payslips[odinalNumber - 1];

                AppResource.Replace(ParameterName.PayslipId, payslip);
                AppResource.Replace(payslip.Id.ToString(), _payslipService.GetDetail(payslip.Id));
                await _navigationService.NavigateAsync(PageName.PayslipDetail);
            });
        }

        public override void OnAppearing()
        {
            IsLoading = true;

            if (AppResource.Get(ParameterName.ListPayslip, out ObservableCollection<Payslip> value))
            {
                Payslips = value;
            }
            else
            {
                Guid employeeId = AppResource.Get<Employee>(ParameterName.Employee).Id;
                var list = _payslipService.GetAll(employeeId);
                Payslips = list;
            }

            IsLoading = false;
        }

        public override void OnDisappearing()
        {
            AppResource.Add(ParameterName.ListPayslip, _payslips);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var payslipId = (Guid?)parameters[ParameterName.PayslipId];
            var isAccepted = (bool?)parameters[ParameterName.Accepted];

            Payslip payslip;
            if (payslipId != null)
            {
                for (int i = 0; i < _payslips.Count; i++)
                {
                    payslip = _payslips[i];
                    if (payslipId.Equals(payslip.Id))
                    {
                        if (isAccepted == true)
                            _payslips[i].Status = "Chấp nhận";
                        else _payslips[i].Status = "Từ chối";
                        Payslips = new ObservableCollection<Payslip>(_payslips);
                        break;
                    }
                }
            }
        }
    }
}
