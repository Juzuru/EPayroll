using EPayroll.FireBases;
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
                            string token = await DependencyService.Get<IFireBaseAuth>().Login(Email, Password);
                            if (token != null)
                            {
                                await _navigationService.PushAsync(new PayslipView());
                            }
                            else
                            {

                            }
                        }
                        catch (Exception e)
                        {
                            LoginErrorMessage = "Sai email hoặc mật khẩu";
                        }
                    }
                });
            }
        }
        #endregion
    }
}
