using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using EarthquakeMonitor.Infrastructure.Interfaces;
using EarthquakeMonitor.Infrastructure.Models;
using EarthquakeMonitor.Infrastructure.Properties;

namespace EarthquakeMonitor.Infrastructure.Services
{
    [Export(typeof(IWorldCitiesLoader))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class WorldCitiesLoader : IWorldCitiesLoader
    {
        public List<City> ReadCities()
        {
            try
            {
                var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(Resources.CitiesSource);

                using (TextReader textReader = new StreamReader(stream))
                {
                    using (var csv = new CsvReader(textReader))
                    {
                        ConfigureCsvReader(csv);
                        var cities = csv.GetRecords<City>().ToList();
                        return cities.Distinct().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureCsvReader(CsvReader csv)
        {
            csv.Configuration.RegisterClassMap<CityClassMap>();
            csv.Configuration.DetectColumnCountChanges = false;
            csv.Configuration.IgnoreHeaderWhiteSpace = true;
            csv.Configuration.IgnoreReadingExceptions = true;
            csv.Configuration.IgnoreQuotes = true;
            csv.Configuration.ReadingExceptionCallback = (ex, row) =>
            {
                // Log the exception and current row information.
                Console.WriteLine(ex.Message + row.ToString());
            };
        }

    }
}
