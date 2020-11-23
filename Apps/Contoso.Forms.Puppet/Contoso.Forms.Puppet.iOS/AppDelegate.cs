// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Contoso.Forms.Puppet.iOS;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics.iOS.Bindings;
using Microsoft.AppCenter.Distribute;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppDelegate))]
namespace Contoso.Forms.Puppet.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IClearCrashClick
    {
        private const string CrashesUserConfirmationStorageKey = "MSACAppCenterCrashesUserConfirmation";

        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Xamarin.Forms.Forms.Init();
            Distribute.DontCheckForUpdatesInDebug();
            MSACAnalytics.SetDelegate(new AnalyticsDelegate());
            LoadApplication(new App());
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        public override void WillEnterForeground(UIApplication uiApplication)
        {
            base.WillEnterForeground(uiApplication);
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }

        public void ClearCrashButton()
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(CrashesUserConfirmationStorageKey);
        }
    }

    public class AnalyticsDelegate : MSACAnalyticsDelegate
    {
        public override void WillSendEventLog(MSACAnalytics analytics, MSACEventLog eventLog)
        {
            AppCenterLog.Debug(App.LogTag, "Will send event");
        }

        public override void DidSucceedSendingEventLog(MSACAnalytics analytics, MSACEventLog eventLog)
        {
            AppCenterLog.Debug(App.LogTag, "Did send event");
        }

        public override void DidFailSendingEventLog(MSACAnalytics analytics, MSACEventLog eventLog, NSError error)
        {
            AppCenterLog.Debug(App.LogTag, "Failed to send event with error: " + error);
        }
    }
}
