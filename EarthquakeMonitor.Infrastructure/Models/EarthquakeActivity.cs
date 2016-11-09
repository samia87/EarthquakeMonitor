using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthquakeMonitor.Infrastructure.Models
{
    public class EarthquakeActivity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Magnitude { get; set; }
        public GeoCoordinate Location { get; set; }
        public List<string> AffectedCities { get; set; }
    }
}
