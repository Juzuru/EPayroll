using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EPayroll.Services
{
    public class NavigationService : INavigationService
    {
        public async Task PushAsync(Page page)
        {
            if (!(Application.Current.MainPage is LoginView))
            {
                var navigationPage = Application.Current.MainPage as NavigationPage;
                if (page != null)
                {
                    await navigationPage.Navigation.PushAsync(page);
                    return;
                }
            }

            Application.Current.MainPage = new NavigationPage(page);
        }
    }

    public interface INavigationService
    {
        Task PushAsync(Page page);
    }
}
