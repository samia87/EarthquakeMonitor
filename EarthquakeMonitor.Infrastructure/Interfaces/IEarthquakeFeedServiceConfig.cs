using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthquakeMonitor.Infrastructure.Interfaces
{
    /// <summary>
    /// interface containing the configuration for running the earthquakefeedservice
    /// </summary>
    public interface IEarthquakeFeedServiceConfig
    {
        int RefreshInterval { get; }
        TimeSpan InitialLoadHistoryTimeSpan { get; }
        int NumberOfAffectedCities { get; }
    }
}
