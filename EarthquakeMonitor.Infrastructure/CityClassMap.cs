using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using EarthquakeMonitor.Infrastructure.Models;

namespace EarthquakeMonitor.Infrastructure
{
    public sealed class CityClassMap : CsvClassMap<City>
    {
        public CityClassMap()
        {
            Map(m => m.UFI).Index(3);
            Map(m => m.Name).Index(6);
            Map(m => m.Latitude).Index(7);
            Map(m => m.Longitude).Index(8);
        }
    }
}
