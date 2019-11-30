using EPayroll.ViewModels;
using EPayroll.ViewModels.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EPayroll
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayslipView : ContentPage
    {
        public PayslipView()
        {
            InitializeComponent();
            BindingContext = ViewModelCreator.Create<PayslipViewModel>();
        }
    }
}