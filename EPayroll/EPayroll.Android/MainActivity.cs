using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.FirebasePushNotification;
using Firebase.Iid;

namespace EPayroll.Droid
{
    [Activity(Label = "EPayroll", Icon = "@mipmap/AppIcon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App(FirebaseInstanceId.Instance.Token));

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }

        //public bool IsPlayServicesAvailable()
        //{
        //    string mess = "";

        //    int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
        //    if (resultCode != ConnectionResult.Success)
        //    {

        //        if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
        //            mess = GoogleApiAvailability.Instance.GetErrorString(resultCode);
        //        else
        //        {
        //            mess = "This device is not supported";
        //            Finish();
        //        }
        //        return false;
        //    }
        //    else
        //    {
        //        mess = "Google Play Services is available.";
        //        return true;
        //    }
        //}

        //void CreateNotificationChannel()
        //{
        //    if (Build.VERSION.SdkInt < BuildVersionCodes.O)
        //    {
        //        // Notification channels are new in API 26 (and not a part of the
        //        // support library). There is no need to create a notification
        //        // channel on older versions of Android.
        //        return;
        //    }

        //    var channel = new NotificationChannel(CHANNEL_ID,
        //                                          "FCM Notifications",
        //                                          NotificationImportance.Default)
        //    {

        //        Description = "Firebase Cloud Messages appear in this channel"
        //    };

        //    var notificationManager = (NotificationManager)GetSystemService(NotificationService);
        //    notificationManager.CreateNotificationChannel(channel);
        //}
    }
}