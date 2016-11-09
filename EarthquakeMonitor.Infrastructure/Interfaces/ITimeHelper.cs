using System;

namespace EarthquakeMonitor.Infrastructure.Interfaces
{
    /// <summary>
    /// Allows to mock the conversion of unix time to datetime
    /// </summary>
    public interface ITimeHelper
    {
        DateTime FromUnixTime(long unixTime);
    }
}