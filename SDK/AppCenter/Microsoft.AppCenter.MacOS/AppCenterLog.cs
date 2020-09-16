namespace Microsoft.AppCenter
{
    using MacOSMessageProvider = Microsoft.AppCenter.MacOS.Bindings.MSLogMessageProvider;
    using MacOSLogger = Microsoft.AppCenter.MacOS.Bindings.MSWrapperLogger;

    public static partial class AppCenterLog
    {
        /// <summary>
        /// The log tag for this SDK. All logs emitted at the SDK level will contain this tag.
        /// </summary>
        public static string LogTag { get; private set; }

        static AppCenterLog()
        {
            LogTag = "AppCenterXamarin";
        }

        public static void Verbose(string tag, string message)
        {
            MacOSMessageProvider msg_provider = () => { return message; };
            MacOSLogger.MSWrapperLog(msg_provider, tag, Microsoft.AppCenter.MacOS.Bindings.MSLogLevel.Verbose);
        }

        public static void Debug(string tag, string message)
        {
            MacOSMessageProvider msg_provider = () => { return message; };
            MacOSLogger.MSWrapperLog(msg_provider, tag, Microsoft.AppCenter.MacOS.Bindings.MSLogLevel.Debug);
        }

        public static void Info(string tag, string message)
        {
            MacOSMessageProvider msg_provider = () => { return message; };
            MacOSLogger.MSWrapperLog(msg_provider, tag, Microsoft.AppCenter.MacOS.Bindings.MSLogLevel.Info);
        }

        public static void Warn(string tag, string message)
        {
            MacOSMessageProvider msg_provider = () => { return message; };
            MacOSLogger.MSWrapperLog(msg_provider, tag, Microsoft.AppCenter.MacOS.Bindings.MSLogLevel.Warning);
        }

        public static void Error(string tag, string message)
        {
            MacOSMessageProvider msg_provider = () => { return message; };
            MacOSLogger.MSWrapperLog(msg_provider, tag, Microsoft.AppCenter.MacOS.Bindings.MSLogLevel.Error);
        }

        public static void Assert(string tag, string message)
        {
            MacOSMessageProvider msg_provider = () => { return message; };
            MacOSLogger.MSWrapperLog(msg_provider, tag, Microsoft.AppCenter.MacOS.Bindings.MSLogLevel.Assert);
        }
    }
}