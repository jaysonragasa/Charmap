using System;
using System.Collections.Generic;

namespace Charmap.Shared.Interfaces
{
    public interface ILogger
    {
        IAppCenterLogger AppCenterLogger { get; set; }
        string Prefix { get; set; }

        void Log(string message, IDictionary<string, string> properties = null, bool AppCenterTrackEvent = true);
        void LogError(Exception exception, string custom_message = "", IDictionary<string, string> properties = null);
        void Start();
        void Stop();
    }
}