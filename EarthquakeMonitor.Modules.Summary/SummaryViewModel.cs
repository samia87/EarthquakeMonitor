using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Models;
using System.Collections.ObjectModel;
using Prism.Events;
using System.ComponentModel.Composition;
using EarthquakeMonitor.Infrastructure.Interfaces;
using EarthquakeMonitor.Infrastructure;

namespace EarthquakeMonitor.Modules.Summary
{
    [Export(typeof(SummaryViewModel))]
    public class SummaryViewModel : BindableBase
    {
        private readonly IEarthquakeFeedService _earthquakeFeedService = null;
        private string _lastMessage;
        public ObservableCollection<EarthquakeActivity> EarthquakeActivities { get; set; }

        [ImportingConstructor]
        public SummaryViewModel(IEarthquakeFeedService earthquakeFeedService, IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator is null");
            }
            if (earthquakeFeedService == null)
            {
                throw new ArgumentNullException("earthquakeFeedService is null");
            }
            _earthquakeFeedService = earthquakeFeedService;
            LastStatus = _earthquakeFeedService.Status;
            EarthquakeActivities = new ObservableCollection<EarthquakeActivity>();
            eventAggregator.GetEvent<EarthquakeActivitiesUpdateEvent>().Subscribe(this.EarthquakeActivitiesChanged, ThreadOption.UIThread);
        }

        private void EarthquakeActivitiesChanged(IList<EarthquakeActivity> obj)
        {
            foreach( var ea in obj)
                EarthquakeActivities.Insert(0,ea);
            LastStatus = _earthquakeFeedService.Status;
        }

        public string LastStatus
        {
            get
            {
                return _lastMessage;
            }
            set
            {
                SetProperty(ref _lastMessage, value);
            }
        }
    }
}
