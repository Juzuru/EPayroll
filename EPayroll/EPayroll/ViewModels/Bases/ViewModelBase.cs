using EPayroll.Services;
using Prism.AppModel;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EPayroll.ViewModels.Bases
{
    public class ViewModelBase : INotifyPropertyChanged, INavigationAware, IPageLifecycleAware
    {
        protected readonly INavigationService _navigationService;

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Previous Properties
        protected bool _isLoading = true;
        protected double _opacity = 0;
        #endregion

        #region Binding Properties
        public bool IsLoading
        {
            get => _isLoading;
            set => SetValue(ref _isLoading, value);
        }
        public double Opacity
        {
            get => _opacity;
            set => SetValue(ref _opacity, value);
        }
        #endregion

        public virtual void OnAppearing()
        {
            
        }

        public virtual void OnDisappearing()
        {
            
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
