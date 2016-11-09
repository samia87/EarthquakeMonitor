using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EarthquakeMonitor.Infrastructure.Interfaces;
using EarthquakeMonitor.Infrastructure.Properties;
using EarthquakeMonitor.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace EarthquakeMonitor.Infrastructure.Tests
{
    [TestClass]
    public class EarthquakeActivityCollectionBuilderTests
    {
        [TestMethod]
        public void TestEarthActivityBuild()
        {
            Mock<ITimeHelper> timeHelperMock = new Mock<ITimeHelper>();
            timeHelperMock.Setup(t => t.FromUnixTime(It.IsAny<long>())).Returns(new DateTime(2012, 01, 01, 01, 01, 01));
            var earthActivityCollectionBuilder = new EarthquakeActivityCollectionBuilder(timeHelperMock.Object);
            Mock<IWorldCitiesRepository> worldCitiesRepositoryMock = new Mock<IWorldCitiesRepository>();
            worldCitiesRepositoryMock.Setup(t => t.GetCitiesNearLocation(It.IsAny<GeoCoordinate>(), It.IsAny<int>()))
                .Returns(new string[] {"testCity1", "testCity2"});
            using (var stream = new StreamReader(
                Assembly.GetExecutingAssembly().GetManifestResourceStream("EarthquakeMonitor.Infrastructure.Tests.earthquaketest.json")))
            {
                JsonTextReader jsonTextReader = new JsonTextReader(stream);
                JsonSerializer serializer = new JsonSerializer();
                dynamic jsonSerializedObject = serializer.Deserialize<dynamic>(jsonTextReader);
                var earthquakeActivities = earthActivityCollectionBuilder.BuildEarthquakeActivities(jsonSerializedObject,
                    worldCitiesRepositoryMock.Object, 2);
                Assert.AreEqual(earthquakeActivities.Count, 2);
            }
        }
    }
}
