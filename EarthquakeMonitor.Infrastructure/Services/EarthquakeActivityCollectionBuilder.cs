using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Helpers;
using EarthquakeMonitor.Infrastructure.Interfaces;
using EarthquakeMonitor.Infrastructure.Models;

namespace EarthquakeMonitor.Infrastructure.Services
{
    [Export(typeof(IEarthquakeActivityCollectionBuilder))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EarthquakeActivityCollectionBuilder : IEarthquakeActivityCollectionBuilder
    {
        private readonly ITimeHelper _timeHelper;
        [ImportingConstructor]
        public EarthquakeActivityCollectionBuilder(ITimeHelper timeHelper)
        {
            _timeHelper = timeHelper;
        }

        public List<EarthquakeActivity> BuildEarthquakeActivities(dynamic earthquakeActivity, IWorldCitiesRepository worldCitiesRepository, int numberOfNearestCities)
        {
            int count = earthquakeActivity.features.Count;
            List<EarthquakeActivity> earthquakeActivities = new List<EarthquakeActivity>();
            for (int i = 0; i < count; i++)
            {
                double mag = earthquakeActivity.features[i].properties.mag;
                long time = earthquakeActivity.features[i].properties.time;
                DateTime dt = _timeHelper.FromUnixTime(time);
                double longitude = earthquakeActivity.features[i].geometry.coordinates[0];
                double latitude = earthquakeActivity.features[i].geometry.coordinates[1];
                var geocoordinate = new GeoCoordinate(latitude, longitude);
                earthquakeActivities.Add(new EarthquakeActivity()
                {
                    DateTime = dt,
                    Magnitude = mag,
                    Location = geocoordinate,
                    AffectedCities = worldCitiesRepository.GetCitiesNearLocation(geocoordinate, numberOfNearestCities).ToList()
                }
                );
            }
            return earthquakeActivities;
        }
    }
}
