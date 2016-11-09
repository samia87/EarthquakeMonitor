using System;
using System.Collections;
using System.Device.Location;

namespace EarthquakeMonitor.Infrastructure.Models
{
    public class City : IEqualityComparer
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string UFI { get; set; }

        public new bool Equals(object x, object y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            
            var xcity = x as City;
            var ycity = y as City;
            if (Object.ReferenceEquals(xcity, null) || Object.ReferenceEquals(ycity, null)) return false;
            if (string.IsNullOrEmpty(xcity.UFI) || string.IsNullOrEmpty(ycity.UFI)) return false;
            if (xcity.UFI == ycity.UFI) return true;
            return false;
        }

        public int GetHashCode(object obj)
        {
            var xcity = obj as City;
            if(!string.IsNullOrEmpty(xcity.UFI))
                return Convert.ToInt32(xcity.UFI.GetHashCode());
            return xcity.Latitude.GetHashCode() ^ xcity.Longitude.GetHashCode() ^ xcity.Name.GetHashCode();
        }
    }
}