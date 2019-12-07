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
            //Container.Resolve<INavigationService>()
            //   .NavigateAsync("EPayroll:///" + PageName.ListPayslip, new NavigationParameters {
            //       {ParameterName.EmployeeId, new Guid("6bd89f97-d2a6-412e-eb90-08d7762cb897") }
            //   });
            Container.Resolve<INavigationService>()
               .NavigateAsync("EPayroll:///" + PageName.UpdateProfile);
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
            containerRegistry.RegisterForNavigation<UpdateProfileView, UpdateProfileViewModel>(PageName.UpdateProfile);
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
