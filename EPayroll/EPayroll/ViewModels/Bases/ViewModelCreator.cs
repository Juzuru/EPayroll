using EPayroll.Services;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPayroll.ViewModels.Bases
{
    public static class ViewModelCreator
    {
        private readonly static TinyIoCContainer _container;

        static ViewModelCreator()
        {
            _container = new TinyIoCContainer();

            #region Register ViewModels
            _container.Register<LoginViewModel>();
            #endregion

            #region Register Services
            _container.Register<IRequestService, RequestService>();
            _container.Register<IAccountService, AccountService>();
            #endregion
        }

        public static TViewModel Create<TViewModel>() where TViewModel : ViewModelBase
        {
            return _container.Resolve<TViewModel>();
        }
    }
}
