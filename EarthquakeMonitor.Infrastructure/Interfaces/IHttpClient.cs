using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EarthquakeMonitor.Infrastructure.Interfaces
{
    /// <summary>
    /// interface to call the webclient, allows for unit testing without calling httpclient
    /// </summary>
    public interface IHttpClient
    {
        Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken) where T : class;
    }
}
