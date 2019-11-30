using EPayroll.Models;
using EPayroll.Services;
using EPayroll.ViewModels.Bases;
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
        private readonly IAccountService _accountService;

        public LoginViewModel(IAccountService accountService, INavigationService _navigationService) : base (_navigationService)
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

        void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            string s = "";
            string ss = ";dawdaw".Equals(s).ToString();
        }

        #region Commands
        public Command LoginCommand
        {
            get
            {
                return new Command(() =>
                {
                    Device.OpenUri(new Uri("https://accounts.google.com/o/oauth2/v2/auth?" +
                        "scope=email%20profile&" +
                        "response_type=code&" +
                        "redirect_uri=com.companyname.epayroll:/oauth2redirect&" +
                        "client_id=268073248035-dtojhfogqktkj22ohraau5vcd7hfli79.apps.googleusercontent.com"));
                    //_navigationService.PushAsync(new GoogleLoginView());

                    /*
                    var uri = "https://accounts.google.com/o/oauth2/v2/auth";
                    var query = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("scope", "email,profile"),
                        new KeyValuePair<string, string>("response_type", "code"),
                        new KeyValuePair<string, string>("redirect_uri", "https://localhost:44382/api/Accounts/GoogleAuthorizedCode"),
                        new KeyValuePair<string, string>("client_id","268073248035-dtojhfogqktkj22ohraau5vcd7hfli79.apps.googleusercontent.com")
                    };

                    
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
                    */
                });
            }
        }
        #endregion
    }
}
