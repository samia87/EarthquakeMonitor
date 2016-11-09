using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Models;

namespace EarthquakeMonitor.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface responsible for reading cities from the worldcities source
    /// </summary>
    public interface IWorldCitiesLoader
    {
        List<City> ReadCities();
    }
}
