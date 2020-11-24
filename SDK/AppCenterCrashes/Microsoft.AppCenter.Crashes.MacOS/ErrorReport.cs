using System;
using Foundation;
using Microsoft.AppCenter.Crashes.MacOS.Bindings;

namespace Microsoft.AppCenter.Crashes
{
    public partial class ErrorReport
    {
        internal ErrorReport(MSACErrorReport msReport)
        {
            // If Id is not null we have loaded the report from the cache
            if (Id != null)
            {
                return;
            }

            Id = msReport.IncidentIdentifier;
            AppStartTime = NSDateToDateTimeOffset(msReport.AppStartTime);
            AppErrorTime = NSDateToDateTimeOffset(msReport.AppErrorTime);
            Device = msReport.Device == null ? null : new Device(msReport.Device);

            AndroidDetails = null;

            iOSDetails = new iOSErrorDetails(msReport.ReporterKey,
                                             msReport.Signal,
                                             msReport.ExceptionName,
                                             msReport.ExceptionReason,
                                             (uint)msReport.AppProcessIdentifier);

            MSACWrapperException wrapperException = MSACWrapperExceptionManager.LoadWrapperExceptionWithUUID(msReport.IncidentIdentifier);
            if (wrapperException != null && wrapperException.ExceptionData != null && wrapperException.ExceptionData.Length > 0)
            {
                StackTrace = CrashesUtils.DeserializeException(wrapperException.ExceptionData.ToArray());
            }
        }

        private DateTimeOffset NSDateToDateTimeOffset(NSDate date)
        {
            DateTime dateTime = (DateTime)date;
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            return dateTime;
        }
    }
}