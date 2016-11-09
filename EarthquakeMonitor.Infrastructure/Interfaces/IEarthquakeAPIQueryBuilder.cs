using System;

namespace EarthquakeMonitor.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface to build the query for the earthquake feed api 
    /// </summary>
    public interface IEarthquakeApiQueryBuilder
    {
        string JsonQueryForTimeRange(DateTime startTime, DateTime endTime);
    }
}