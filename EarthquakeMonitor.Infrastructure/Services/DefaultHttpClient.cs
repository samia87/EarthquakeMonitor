using EarthquakeMonitor.Infrastructure.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EarthquakeMonitor.Infrastructure.Services
{
    [Export(typeof(IHttpClient))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DefaultHttpClient : IHttpClient
    {
        private readonly HttpClient _client = null;

        
        public DefaultHttpClient()
        {
            _client = new HttpClient();
        }

        public Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken) where T : class
        {
            return GetAsync<T>(new Uri(uri), cancellationToken);
        }

        async Task<T> GetAsync<T>(Uri path, CancellationToken cancellationToken) where T : class
        {
            T retVal = default(T);
            try
            {
                HttpResponseMessage response = await _client.GetAsync(path, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    retVal = await response.Content.ReadAsAsync<T>(cancellationToken);
                }
            }
            catch(Exception ex)
            {
                //todo: bubble up exception
                Debug.WriteLine(ex.Message);
            }
            return retVal;
        }
    }
}
