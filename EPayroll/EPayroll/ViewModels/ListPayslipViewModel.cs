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
            Payslips = new ObservableCollection<Payslip>();
        }

        #region Previous Properties
        private bool _isLoading = true;
        #endregion

        #region Binding Properties
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetValue(ref _isLoading, value); }
        }
        #endregion

        public ObservableCollection<Payslip> Payslips { get; private set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Task.Run(async () => 
            {
                try
                {
                    Guid employeeId = (Guid)parameters[ParameterName.EmployeeId];
                    var list = await _payslipService.GetAllAsync(employeeId);

                    Device.BeginInvokeOnMainThread(() => 
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            Payslips.Add(list[i]);
                        }
                    });

                    IsLoading = false;
                }
                catch (Exception e)
                {

                }
            });
        }
    }
}
