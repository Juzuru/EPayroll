using EPayroll.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EPayroll
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
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

        public async static void GetGoogleToken(string code)
        {
            GoogleTokenModel googleTokenModel;
            using (var httpClient = new HttpClient())
            {
                var uri = "https://www.googleapis.com/oauth2/v4/token";
                var list = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("client_id", "268073248035-dtojhfogqktkj22ohraau5vcd7hfli79.apps.googleusercontent.com"),
                    new KeyValuePair<string, string>("client_secret", ""),
                    new KeyValuePair<string, string>("redirect_uri", "com.companyname.epayroll:/oauth2redirect"),
                    new KeyValuePair<string, string>("grant_type", "authorization_code")
                };

                var response = await httpClient.PostAsync(uri, new FormUrlEncodedContent(list));
                try
                {
                    response.EnsureSuccessStatusCode();
                    googleTokenModel = JsonConvert.DeserializeObject<GoogleTokenModel>(await response.Content.ReadAsStringAsync());
                }
                catch (Exception)
                {

                    throw;
                }
            }

            using(var httpClient = new HttpClient())
            {
                var uri = "https://www.googleapis.com/oauth2/v3/tokeninfo?" +
                    "access_token=" + googleTokenModel.Access_token;
                httpClient.DefaultRequestHeaders.Add("Authorization", googleTokenModel.Token_type + " " + googleTokenModel.Access_token);

                var response = await httpClient.GetAsync(uri);
                try
                {
                    response.EnsureSuccessStatusCode();
                    googleUserInfoModel = JsonConvert.DeserializeObject<GoogleUserInfoModel>(await response.Content.ReadAsStringAsync());

                    Current.MainPage = new NavigationPage(new PayslipView());
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static GoogleUserInfoModel googleUserInfoModel;
        public static string Token;
    }
}
