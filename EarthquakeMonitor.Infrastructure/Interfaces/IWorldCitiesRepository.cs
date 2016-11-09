using EarthquakeMonitor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Helpers;

namespace EarthquakeMonitor.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository for getting all cities and cities nearest to a location
    /// </summary>
    public interface IWorldCitiesRepository
    {
        IEnumerable<City> GetCities();
        IEnumerable<string> GetCitiesNearLocation(GeoCoordinate location, int countOfCities);
    }
}
