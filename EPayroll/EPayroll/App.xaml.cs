using EPayroll.Models;
using EPayroll.Resources;
using EPayroll.ServiceModels;
using EPayroll.Services;
using EPayroll.ViewModels;
using EPayroll.Views;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EPayroll
{
    public partial class App
    {
        private readonly string _token;

        public App(string token) : this(null, token) { _token = token; }

        public App(IPlatformInitializer initializer, string token) : base(initializer)
        {
            AppResource.Add(ParameterName.FCMToken, token);
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            if (AppResource.Get(ParameterName.Employee, out Employee employee))
            {
                Guid? employeeId = Container.Resolve<IEmployeeService>().CheckUser(employee.Email, employee.UserUID);
                Container.Resolve<INavigationService>()
                    .NavigateAsync("EPayroll:///" + PageName.Navigation + "/" + PageName.ListPayslip, new NavigationParameters
                    {
                        {ParameterName.EmployeeId, employeeId }
                    });
            }
            else
            {
                Container.Resolve<INavigationService>()
                    .NavigateAsync("EPayroll:///" + PageName.Login);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) 
        {
            #region Services
            containerRegistry.Register<IEmployeeService, EmployeeService>();
            containerRegistry.Register<IFirebaseService, FirebaseService>();
            containerRegistry.Register<IPayslipService, PayslipService>();
            containerRegistry.Register<IRequestService, RequestService>();
            #endregion

            #region Navigations
            containerRegistry.RegisterForNavigation<ListPayslipView, ListPayslipViewModel>(PageName.ListPayslip);
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>(PageName.Login);
            containerRegistry.RegisterForNavigation<ProfileView, ProfileViewModel>(PageName.ProfileView);
            containerRegistry.RegisterForNavigation<NavigationPage>(PageName.Navigation);
            containerRegistry.RegisterForNavigation<PayslipDetailView, PayslipDetailViewModel>(PageName.PayslipDetail);
            #endregion
        }

        protected override void OnStart()
        {
            // Handle when your app starts


            CrossFirebasePushNotification.Current.Subscribe("General");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (sender, e) =>
            {
                if (AppResource.Get(ParameterName.Employee, out Employee employee))
                {
                    Container.Resolve<IFirebaseService>().SendFCMToken(employee.Id, _token);
                }
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (sender, e) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in e.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }


            };

            CrossFirebasePushNotification.Current.OnNotificationAction += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");

                if (!string.IsNullOrEmpty(e.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {e.Identifier}");
                    foreach (var data in e.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }

                }

            };
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
