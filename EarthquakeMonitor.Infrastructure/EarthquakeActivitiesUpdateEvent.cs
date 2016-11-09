using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Models;

namespace EarthquakeMonitor.Infrastructure
{
    public class EarthquakeActivitiesUpdateEvent : PubSubEvent<IList<EarthquakeActivity>>
    {
    }
}
