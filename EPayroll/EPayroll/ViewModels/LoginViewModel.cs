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

            Email = "toanldse63050@fpt.edu.vn";
            Password = "toanld";
        }


        #region Previous Values
        private string _loginErrorMessage;
        #endregion

        #region Binding Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginErrorMessage
        {
            get { return _loginErrorMessage; }
            set { SetValue(ref _loginErrorMessage, value); }
        }
        public ImageSource LogoLogin
        {
            get { return ImageSource.FromResource("EPayroll.Images.LogoLogin.png"); }
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
                                Guid? employeeId = await _employeeService.CheckUserAsync(Email, userUID);
                                
                                if (employeeId != null)
                                {
                                    await _navigationService.NavigateAsync(PageName.ListPayslip, new NavigationParameters
                                    {
                                        {ParameterName.EmployeeId, employeeId }
                                    });
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
    }
}
