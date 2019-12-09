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
    public class ListPayslipViewModel : ViewModelBase
    {
        private readonly IPayslipService _payslipService;

        public ListPayslipViewModel(INavigationService navigationService, IPayslipService payslipService) : base(navigationService)
        {
            _payslipService = payslipService;
        }

        #region Previous Properties
        private bool _isLoading = true;
        private Payslip _selectedPayslip;
        private ObservableCollection<Payslip> _payslips;
        #endregion

        #region Binding Properties
        public bool IsLoading
        {
            get => _isLoading;
            set => SetValue(ref _isLoading, value);
        }
        public Payslip SelectedPayslip
        {
            get => _selectedPayslip;
            set
            {
                _selectedPayslip = value;

                if (_selectedPayslip.OrdinalNumber == -10)
                    _selectedPayslip = _payslips[_payslips.Count - 2];
                else _selectedPayslip = _payslips[_selectedPayslip.OrdinalNumber - 2];
                
                AppResource.Replace(ParameterName.PayslipId, _selectedPayslip.Id);
                SetValue(ref _selectedPayslip, null);

                _navigationService.NavigateAsync(PageName.PayslipDetail);
            }
        }
        public ObservableCollection<Payslip> Payslips
        {
            get => _payslips;
            set => SetValue(ref _payslips, value);
        }
        #endregion

        public override void OnAppearing()
        {
            if (AppResource.Get(ParameterName.ListPayslip, out ObservableCollection<Payslip> value))
            {
                Payslips = value;
            }
            else
            {
                Guid employeeId = AppResource.Get<Employee>(ParameterName.Employee).Id;
                
                var list = _payslipService.GetAll(employeeId);
                list.Add(new Payslip
                {
                    OrdinalNumber = -10,
                    PaySlipCode = "Kì lương",
                    Amount = -11,
                    Status = "Trạng thái"
                });

                Payslips = list;
            }
        }

        public override void OnDisappearing()
        {
            AppResource.Add(ParameterName.ListPayslip, _payslips);
        }
    }
}
