using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthquakeMonitor.Infrastructure.Interfaces
{
    /// <summary>
    /// Main interface to load the past one hour earthquake activity and monitor and notify on recent activity
    /// </summary>
    public interface IEarthquakeFeedService
    {
        string Status { get; set; }
    }
}
