using EarthquakeMonitor.Infrastructure.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Helpers;
using EarthquakeMonitor.Infrastructure.Models;

namespace EarthquakeMonitor.Infrastructure.Services
{
    [Export(typeof(IEarthquakeFeedService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EarthquakeFeedService : IEarthquakeFeedService
    {
        private Timer _timer;

        DateTime _timeFeedStarted;
        DateTime _lastFeedRetrievalTime = DateTime.UtcNow;

        private readonly IEventAggregator _eventAggregator;
        private readonly IHttpClient _httpClient;
        private readonly IWorldCitiesRepository _worldCitiesRepository;
        private readonly IEarthquakeApiQueryBuilder _earthquakeApiQueryBuilder;
        private readonly IEarthquakeFeedServiceConfig _config;
        private readonly IEarthquakeActivityCollectionBuilder _earthquakeActivityCollectionBuilder;

        private readonly CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _initialLoadHistoryEarthquakeActivitiesTask;

        public string Status { get; set; }

        [ImportingConstructor]
        public EarthquakeFeedService(IEventAggregator eventAggregator,
                                    IHttpClient httpClient,
                                    IWorldCitiesRepository worldCitiesRepository,
                                    IEarthquakeApiQueryBuilder earthquakeApiQueryBuilder,
                                    IEarthquakeFeedServiceConfig config,
                                    IEarthquakeActivityCollectionBuilder earthquakeActivityCollectionBuilder)
        {
            _eventAggregator = eventAggregator;
            _earthquakeActivityCollectionBuilder = earthquakeActivityCollectionBuilder;
            _httpClient = httpClient;
            _worldCitiesRepository = worldCitiesRepository;
            _earthquakeApiQueryBuilder = earthquakeApiQueryBuilder;
            _config = config;
            _earthquakeActivityCollectionBuilder = earthquakeActivityCollectionBuilder;

            
            _timeFeedStarted = _lastFeedRetrievalTime.Subtract(config.InitialLoadHistoryTimeSpan);
            
            _initialLoadHistoryEarthquakeActivitiesTask =
                Task.Run(() => LoadEarthQuakeHistory(_timeFeedStarted, _lastFeedRetrievalTime));
            
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            
            _timer = new Timer(TimerTick);
            _timer.Change(config.RefreshInterval, config.RefreshInterval);

            Status = string.Format("Past {0} hrs activity retrieval started", config.InitialLoadHistoryTimeSpan.TotalHours);
        }

        private async void LoadEarthQuakeHistory(DateTime startTime, DateTime endTime)
        {
            string uri = _earthquakeApiQueryBuilder.JsonQueryForTimeRange(startTime, endTime);
            dynamic earthquakeActivity = await _httpClient.GetAsync<dynamic>(uri, _cancellationToken);
            var earthquakeActivities = _earthquakeActivityCollectionBuilder.BuildEarthquakeActivities(earthquakeActivity, _worldCitiesRepository, _config.NumberOfAffectedCities);
            Status = string.Format("Last updated at {0} with {1}", endTime.ToString(), uri);
            _eventAggregator.GetEvent<EarthquakeActivitiesUpdateEvent>().Publish(earthquakeActivities);
            
        }

        private void TimerTick(object state)
        {
            Debug.WriteLine("TimerTick called: " + DateTime.UtcNow.ToString("yy-MM-ddTHH:mm:ss"));
            _timeFeedStarted = _lastFeedRetrievalTime;
            _lastFeedRetrievalTime = DateTime.UtcNow;
            _initialLoadHistoryEarthquakeActivitiesTask.Wait(_cancellationToken);
            _initialLoadHistoryEarthquakeActivitiesTask =
                Task.Run(() => LoadEarthQuakeHistory(_timeFeedStarted, _lastFeedRetrievalTime));
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _timer?.Dispose();
            _timer = null;
            _cancellationTokenSource.Cancel();
            
            try
            {
                if (_initialLoadHistoryEarthquakeActivitiesTask == null) return;
                _initialLoadHistoryEarthquakeActivitiesTask.Wait(_cancellationToken);
            }
            catch (AggregateException e)
            {
                LogAggregateException(e);
            }
            finally
            {
                _initialLoadHistoryEarthquakeActivitiesTask = null;
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private static void LogAggregateException(AggregateException e)
        {
            foreach (var v in e.InnerExceptions)
            {
                if (v is TaskCanceledException)
                    Console.WriteLine("Task Canceled Exception: Task{0}",
                        (v as TaskCanceledException).Task.Id);
                else
                    Console.WriteLine("Exception: {0}", v.GetType().Name);
            }
        }

        // Use C# destructor syntax for finalization code.
        ~EarthquakeFeedService()
        {
            Dispose(false);
        }

        #endregion
    }
}
