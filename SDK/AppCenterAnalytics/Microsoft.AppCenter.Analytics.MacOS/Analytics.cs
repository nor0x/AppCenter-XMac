using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Foundation;

namespace Microsoft.AppCenter.Analytics
{
    using System.Linq;
    using System.Threading.Tasks;
    using MacOSAnalytics = MacOS.Bindings.MSACAnalytics;

    /// <summary>
    /// Analytics feature.
    /// </summary>
    public class Analytics : AppCenterService
    {
        internal Analytics()
        {
        }

        /// <summary>
        /// Internal SDK property not intended for public use.
        /// </summary>
        /// <value>
        /// The MacOS SDK Analytics bindings type.
        /// </value>
        [Preserve]
        public static Type BindingType => typeof(MacOSAnalytics);

        /// <summary>
        /// Check whether the Analytics service is enabled or not.
        /// </summary>
        /// <returns>A task with result being true if enabled, false if disabled.</returns>
        public static Task<bool> IsEnabledAsync()
        {
            return Task.FromResult(MacOSAnalytics.IsEnabled());
        }

        /// <summary>
        /// Enable or disable the Analytics service.
        /// </summary>
        /// <returns>A task to monitor the operation.</returns>
        public static Task SetEnabledAsync(bool enabled)
        {
            MacOSAnalytics.SetEnabled(enabled);
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Pause the Analytics service.
        /// </summary>
        internal static void Pause()
        {
            MacOSAnalytics.Pause();
        }

        /// <summary>
        /// Resume the Analytics service.
        /// </summary>
        internal static void Resume()
        {
            MacOSAnalytics.Resume();
        }
        /// <summary>
        /// Enable or disable automatic page tracking.
        /// Set this to false to if you plan to call <see cref="TrackPage"/> manually.
        /// </summary>
        //public static bool AutoPageTrackingEnabled
        //{
        //  get { return MacOSAnalytics.IsAutoPageTrackingEnabled(); }
        //  set { MacOSAnalytics.SetAutoPageTrackingEnabled(value); }
        //}

        /// <summary>
        /// Track a custom event.
        /// </summary>
        /// <param name="name">An event name.</param>
        /// <param name="properties">Optional properties.</param>
        public static void TrackEvent(string name, [Optional] IDictionary<string, string> properties)
        {
            if (properties != null)
            {
                MacOSAnalytics.TrackEvent(name, StringDictToNSDict(properties));
                return;
            }
            MacOSAnalytics.TrackEvent(name);
        }

        ///// <summary>
        ///// Track a custom page.
        ///// </summary>
        ///// <param name="name">A page name.</param>
        ///// <param name="properties">Optional properties.</param>
        //public static void TrackPage(string name, [Optional] IDictionary<string, string> properties)
        //{
        //  if (properties != null)
        //  {
        //      MacOSAnalytics.TrackPage(name, StringDictToNSDict(properties));
        //      return;
        //  }
        //  MacOSAnalytics.TrackPage(name);
        //}

        internal static void UnsetInstance()
        {
            MacOSAnalytics.ResetSharedInstance();
        }
        
        private static NSDictionary StringDictToNSDict(IDictionary<string, string> dict)
        {
            return NSDictionary.FromObjectsAndKeys(dict.Values.ToArray(), dict.Keys.ToArray());
        }
    }
}