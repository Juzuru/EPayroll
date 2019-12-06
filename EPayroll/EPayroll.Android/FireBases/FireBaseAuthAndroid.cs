using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EPayroll.FireBases;
using EPayroll.Droid.FireBases;
using Firebase.Auth;
using Java.Lang;
using Xamarin.Forms;

[assembly: Dependency(typeof(FireBaseAuthAndroid))]
namespace EPayroll.Droid.FireBases
{
    public class FireBaseAuthAndroid : IFireBaseAuth
    {
        public async Task<string> Login(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                //var token = await user.User.GetIdTokenAsync(false);
                var userUID = user.User.Uid;

                return userUID;
            }
            catch (FirebaseAuthInvalidUserException)//Wrong email
            {
                return null;
            }
            catch (FirebaseAuthInvalidCredentialsException)//Wrong password
            {
                return null;
            }
            catch (IllegalArgumentException)//Email or password is null
            {
                return null;
            }
        }
    }
}