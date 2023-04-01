using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;

using Android.Gestures;
using Android.Nfc;
using Android.Util;
using CN.Jpush.Android.Api;
using CN.Jpush.Android.Service;
using System.Runtime.Remoting.Contexts;

namespace PushDemo.Droid
{
    [Activity(Label = "PushDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            initPushNotification();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void initPushNotification()
        {
            IntentFilter filter = new IntentFilter();
            filter.AddAction(JPushInterface.ActionNotificationOpened);
            filter.AddAction(JPushInterface.ActionNotificationReceived);
            filter.AddAction(JPushInterface.ActionMessageReceived);
            filter.AddAction(JPushInterface.ActionRegistrationId);
            filter.AddAction(JPushInterface.ActionConnectionChange);
            NotificationReceiver receiver = new NotificationReceiver();
            RegisterReceiver(receiver, filter);
            JPushInterface.SetDebugMode(true);
            JPushInterface.Init(this.ApplicationContext);

            string regID = JPushInterface.GetRegistrationID(this.ApplicationContext);
            Console.WriteLine($"<------registrationID:{regID}------------>");
        }

        [BroadcastReceiver(Exported =true)]
        [IntentFilter(new string[] { "cn.jpush.android.intent.REGISTRATION" }, Categories = new string[] { "com.iwaitu.pushdemo" })]
        [IntentFilter(new string[] { "cn.jpush.android.intent.MESSAGE_RECEIVED" }, Categories = new string[] { "com.iwaitu.pushdemo" })]
        [IntentFilter(new string[] { "cn.jpush.android.intent.NOTIFICATION_RECEIVED" }, Categories = new string[] { "com.iwaitu.pushdemo" })]
        [IntentFilter(new string[] { "cn.jpush.android.intent.NOTIFICATION_OPENED" }, Categories = new string[] { "com.iwaitu.pushdemo" })]
        [IntentFilter(new string[] { "cn.jpush.android.intent.CONNECTION" }, Categories = new string[] { "com.iwaitu.pushdemo" })]
        public class NotificationReceiver : JPushMessageReceiver
        {
            //仅第一次运行app时激活
            public override void OnRegister(Android.Content.Context p0, string p1)
            {
                base.OnRegister(p0, p1);
                string regID = JPushInterface.GetRegistrationID(p0);
                Console.WriteLine($"<------registrationID:{regID}------------>");
            }

            public override void OnMessage(Android.Content.Context p0, CustomMessage p1)
            {
                base.OnMessage(p0, p1);
            }

            //默认使用这个
            public override void OnNotifyMessageArrived(Android.Content.Context p0, NotificationMessage p1)
            {
                base.OnNotifyMessageArrived(p0, p1);
            }
            
            public override void OnConnected(Android.Content.Context p0, bool p1)
            {
                base.OnConnected(p0, p1);
            }
        }

    }
}