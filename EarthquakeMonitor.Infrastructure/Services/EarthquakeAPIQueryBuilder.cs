using EarthquakeMonitor.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Properties;

namespace EarthquakeMonitor.Infrastructure.Services
{
    [Export(typeof(IEarthquakeApiQueryBuilder))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EarthquakeApiQueryBuilder : IEarthquakeApiQueryBuilder
    {
        private static readonly string queryFormat = Resources.QueryFormat;
        private const string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
        //todo: watchout for duplicates
        public string JsonQueryForTimeRange(DateTime startTime, DateTime endTime)
        {
            return string.Format(queryFormat, "geojson", startTime.ToString(dateTimeFormat), endTime.ToString(dateTimeFormat));
        }
    }
}
