using EPayroll.FireBases;
using EPayroll.Models;
using EPayroll.Resources;
using EPayroll.Services;
using EPayroll.ViewModels.Bases;
using Prism.Navigation;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EPayroll.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPayslipService _payslipService;
        private readonly IFirebaseService _firebaseService;

        public LoginViewModel(IEmployeeService employeeService, IPayslipService payslipService, IFirebaseService firebaseService, INavigationService navigationService) : base(navigationService)
        {
            _employeeService = employeeService;
            _payslipService = payslipService;
            _firebaseService = firebaseService;

            Email = "toanldse63050@fpt.edu.vn";
            Password = "toanld";
        }

        #region Previous Values
        private string _loginErrorMessage;
        private string _email;
        private string _password;
        #endregion

        #region Binding Properties
        public string Email
        {
            get => _email;
            set => SetValue(ref _email, value);
        }
        public string Password
        {
            get => _password;
            set => SetValue(ref _password, value);
        }
        public string LoginErrorMessage
        {
            get => _loginErrorMessage;
            set => SetValue(ref _loginErrorMessage, value);
        }
        public ImageSource LogoLogin
        {
            get => ImageSource.FromResource("EPayroll.Images.AppIcon.png");
        }
        #endregion

        #region Commands
        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
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

                    if (string.IsNullOrEmpty(Email))
                    {
                        LoginErrorMessage = "Email không được để trống";
                    }
                    else if (string.IsNullOrEmpty(Password))
                    {
                        LoginErrorMessage = "Mật khẩu không được để trống";
                    }
                    else
                    {
                        try
                        {
                            string userUID = await DependencyService.Get<IFireBaseAuth>().Login(Email, Password);
                            if (userUID != null)
                            {
                                Guid? employeeId = _employeeService.CheckUser(Email, userUID);
                                
                                if (employeeId != null)
                                {
                                    Employee employee = _employeeService.GetById(new Guid(employeeId.ToString()));
                                    employee.UserUID = userUID;
                                    employee.Email = Email;
                                    employee.EmployeeCode = Email.Substring(0, Email.IndexOf("@"));

                                    _firebaseService.SendFCMToken(employee.Id, AppResource.Get<string>(ParameterName.FCMToken));
                                    AppResource.Add(ParameterName.Employee, employee);
                                    AppResource.Add(ParameterName.ListPayslip, _payslipService.GetAll(employee.Id));

                                    await _navigationService.NavigateAsync("EPayroll:///" + PageName.Navigation + "/" + PageName.ListPayslip);
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                LoginErrorMessage = "Sai email hoặc mật khẩu";
                            }
                        }
                        catch (Exception)
                        {
                            
                        }
                    }
                });
            }
        }
        #endregion

        public override void OnAppearing()
        {
            //Email = AppResource.Get<string>(ParameterName.Email);
            //Password = AppResource.Get<string>(ParameterName.Password);
            Task.Run(() =>
            {
                Opacity = 1;
                while (Opacity >= 0)
                {
                    Thread.Sleep(50);
                    Opacity -= 0.1;
                }
            });
            IsLoading = false;
        }

        public override void OnDisappearing()
        {
            AppResource.Add(ParameterName.Email, _email);
            AppResource.Add(ParameterName.Password, _password);
        }
    }
}
