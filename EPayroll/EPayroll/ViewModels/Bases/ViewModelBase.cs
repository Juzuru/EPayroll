using EPayroll.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace EPayroll.ViewModels.Bases
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        #region Previous Values
        protected double _basicWidth;
        #endregion

        #region Binding Properties
        public double BasicWidth
        {
            get { return _basicWidth; }
        }
        #endregion
    }
}
