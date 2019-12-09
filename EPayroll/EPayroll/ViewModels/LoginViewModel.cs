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
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EPayroll.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IEmployeeService _employeeService;

        public LoginViewModel(IEmployeeService employeeService, INavigationService _navigationService) : base (_navigationService)
        {
            _employeeService = employeeService;

            _email = "toanldse63050@fpt.edu.vn";
            _password = "toanld";
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
            get => ImageSource.FromResource("EPayroll.Images.LogoApp.png");
        }
        #endregion

        #region Commands
        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
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
                                    AppResource.Add(ParameterName.Employee, employee);

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
        }

        public override void OnDisappearing()
        {
            AppResource.Add(ParameterName.Email, _email);
            AppResource.Add(ParameterName.Password, _password);
        }
    }
}
