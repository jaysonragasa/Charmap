using System;
using System.Collections.Generic;

namespace Charmap.Shared.Interfaces
{
    public interface IAppCenterLogger
    {
        void TrackEvent(string name, IDictionary<string, string> properties = null);
        void TrackError(Exception exception, IDictionary<string, string> properties = null);
    }
}