using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Interfaces;

namespace EarthquakeMonitor.Infrastructure.Services
{
    [Export(typeof(IEarthquakeFeedServiceConfig))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EarthquakeFeedServiceConfig : IEarthquakeFeedServiceConfig
    {
        public int RefreshInterval { get; private set; }
        public TimeSpan InitialLoadHistoryTimeSpan { get; private set; }
        public int NumberOfAffectedCities { get; }
        [ImportingConstructor]
        public EarthquakeFeedServiceConfig()
        {
            RefreshInterval = 30000;
            InitialLoadHistoryTimeSpan = new TimeSpan(0,1,0,0);
            NumberOfAffectedCities = 3;
        }
    }
}
