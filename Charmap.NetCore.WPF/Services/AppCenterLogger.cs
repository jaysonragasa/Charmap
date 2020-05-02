using Charmap.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Charmap.NetCore.WPF.Services
{
    public class AppCenterLogger : IAppCenterLogger
    {
        public void TrackError(Exception exception, IDictionary<string, string> properties = null)
        {
            Microsoft.AppCenter.Crashes.Crashes.TrackError(exception, properties);
        }

        public void TrackEvent(string name, IDictionary<string, string> properties = null)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(name, properties);
        }
    }
}