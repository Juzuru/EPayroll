using EPayroll.Models;
using EPayroll.Services;
using EPayroll.ViewModels.Bases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EPayroll.ViewModels
{
    public class PayslipViewModel : ViewModelBase
    {
        private readonly IPayslipService _payslipService;

        public PayslipViewModel(INavigationService navigationService, IAccountService accountService, IPayslipService payslipService) : base(navigationService)
        {
            _payslipService = payslipService;
        }
    }
}
