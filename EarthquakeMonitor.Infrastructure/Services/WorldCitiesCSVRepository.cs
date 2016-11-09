using EarthquakeMonitor.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Models;
using System.IO;
using System.Device.Location;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using CsvHelper;
using EarthquakeMonitor.Infrastructure.Helpers;
using EarthquakeMonitor.Infrastructure.Properties;

namespace EarthquakeMonitor.Infrastructure.Services
{
    [Export(typeof(IWorldCitiesRepository))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class WorldCitiesCsvRepository : IWorldCitiesRepository
    {
        private IList<City> _cities;
        private readonly Task<List<City>> _loadCSVReaderTask;

        [ImportingConstructor]
        public WorldCitiesCsvRepository(IWorldCitiesLoader citiesLoader)
        {
            _loadCSVReaderTask = Task.Run(() => citiesLoader.ReadCities());
        }

        public IEnumerable<City> GetCities()
        {
            if (!_loadCSVReaderTask.IsCompleted)
            {
                _loadCSVReaderTask.Wait();

                _cities = _loadCSVReaderTask.Result;
            }
            return _cities;
        }
        

        public IEnumerable<string> GetCitiesNearLocation(GeoCoordinate location, int countOfCities)
        {
            return GetCities().OrderBy(c => new GeoCoordinate(c.Latitude, c.Longitude).GetDistanceTo(location))
                    .Select(c => c.Name).Take(countOfCities);
        }

        
    }
}
