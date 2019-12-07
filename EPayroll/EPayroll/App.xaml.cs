using EPayroll.Resources;
using EPayroll.Services;
using EPayroll.ViewModels;
using EPayroll.Views;
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
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            //Container.Resolve<INavigationService>()
            //  .NavigateAsync("EPayroll:///" + PageName.Navigation + "/" + PageName.Login);
            //RContainer.Resolve<INavigationService>()
            //    .NavigateAsync("EPayroll:///" + PageName.ListPayslip, new NavigationParameters {
            //        {ParameterName.EmployeeId, new Guid("515f1407-f98f-4f15-8459-08d778b630cb") }
            //    });
            Container.Resolve<INavigationService>()
                .NavigateAsync("EPayroll:///" + PageName.ProfileView);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) 
        {
            #region Services
            containerRegistry.Register<IEmployeeService, EmployeeService>();
            containerRegistry.Register<IPayslipService, PayslipService>();
            containerRegistry.Register<IRequestService, RequestService>();
            #endregion

            #region Navigations
            containerRegistry.RegisterForNavigation<ListPayslipView, ListPayslipViewModel>(PageName.ListPayslip);
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>(PageName.Login);
            containerRegistry.RegisterForNavigation<ProfileView, ProfileViewModel>(PageName.ProfileView);
            containerRegistry.RegisterForNavigation<NavigationPage>(PageName.Navigation);
            #endregion
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
