using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Models;

namespace EarthquakeMonitor.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface to build the Earthquake Activity Model 
    /// </summary>
    public interface IEarthquakeActivityCollectionBuilder
    {
        List<EarthquakeActivity> BuildEarthquakeActivities(dynamic earthquakeActivity,
            IWorldCitiesRepository worldCitiesRepository, int numberOfNearestCities);

    }
}
