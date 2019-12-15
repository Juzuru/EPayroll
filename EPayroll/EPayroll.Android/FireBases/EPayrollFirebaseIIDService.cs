using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;

namespace EPayroll.Droid.FireBases
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    class EPayrollFirebaseIIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            //FirebaseInstanceId.Instance.Token;
        }
    }
}