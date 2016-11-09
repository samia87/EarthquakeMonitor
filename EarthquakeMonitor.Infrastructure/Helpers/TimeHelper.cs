using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Interfaces;

namespace EarthquakeMonitor.Infrastructure.Helpers
{
    [Export(typeof(ITimeHelper))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TimeHelper : ITimeHelper
    {
        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTime);
        }

        public Tuple<double, double> LongitudeRange(double longitude, double distance)
        {
            double delta = distance / Math.Abs(69 * Math.Cos(DegreeToRadian(longitude)));
            double min = longitude - delta;
            double max = longitude + delta;
            return new Tuple<double, double>(min, max);
        }

        public Tuple<double, double> LatitudeRange(double latitude, double distance)
        {
            double delta = distance / Math.Abs(69);
            double min = latitude - delta;
            double max = latitude + delta;
            return new Tuple<double, double>(min, max);
        }

        public double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
