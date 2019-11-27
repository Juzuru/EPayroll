using EPayroll.Models;
using EPayroll.Services;
using EPayroll.ViewModels.Bases;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EPayroll.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAccountService _accountService;

        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region Previous Values
        private string _employeeId;
        private string _password;
        private string _loginErrorMessage;
        #endregion

        #region Binding Properties
        public string EmployeeId
        {
            get { return _employeeId; }
            set { SetValue(ref _employeeId, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }
        public string LoginErrorMessage
        {
            get { return _loginErrorMessage; }
            set { SetValue(ref _loginErrorMessage, value); }
        }
        #endregion

        #region Commands
        public Command LoginCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (string.IsNullOrEmpty(_employeeId) || string.IsNullOrEmpty(_password))
                    {
                        LoginErrorMessage = "Employee ID and Password cannot be NULL!!!";
                    }
                    else
                    {
                        try
                        {
                            AccountViewModel accountViewModel = _accountService.CheckLogin(_employeeId, _password);
                            if (accountViewModel.IsRemove)
                            {
                                LoginErrorMessage = _employeeId + " Remove!!!";
                            }
                            else
                            {
                                LoginErrorMessage = _employeeId + " Success!!!";
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.Message.Contains("Unauthorized"))
                            {
                                LoginErrorMessage = "Invalid Employee ID or Password!!!";
                            }
                        }
                    }
                });
            }
        }
        #endregion
    }
}
