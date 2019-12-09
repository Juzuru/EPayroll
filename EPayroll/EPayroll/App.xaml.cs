using EPayroll.Models;
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
            //Container.Resolve<INavigationService>()
            //    .NavigateAsync("EPayroll:///" + PageName.ListPayslip, new NavigationParameters {
            //        {ParameterName.EmployeeId, new Guid("515f1407-f98f-4f15-8459-08d778b630cb") }
            //    });
            //Container.Resolve<INavigationService>()
            //    .NavigateAsync("EPayroll:///" + PageName.PayslipDetail, new NavigationParameters {
            //        {ParameterName.PayslipId, new Guid("11aa2f49-177d-4978-9a97-08d77956e2a2") }
            //    });
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
            containerRegistry.RegisterForNavigation<NavigationPage>(PageName.Navigation);
            containerRegistry.RegisterForNavigation<PayslipDetailView, PayslipDetailViewModel>(PageName.PayslipDetail);
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
