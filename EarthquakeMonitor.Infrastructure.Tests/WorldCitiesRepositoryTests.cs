using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EarthquakeMonitor.Infrastructure.Interfaces;
using EarthquakeMonitor.Infrastructure.Models;
using EarthquakeMonitor.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EarthquakeMonitor.Infrastructure.Tests
{
    [TestClass]
    public class WorldCitiesRepositoryTests
    {
        [TestMethod]
        public void TestGetCities()
        {
            Mock<IWorldCitiesLoader> mockCitiesLoader = new Mock<IWorldCitiesLoader>();
            mockCitiesLoader.Setup(t => t.ReadCities()).Returns(new List<City>() {new City(), new City()});
            WorldCitiesCsvRepository repository = new WorldCitiesCsvRepository(mockCitiesLoader.Object);
            Assert.AreEqual(repository.GetCities().Count(),2);
        }

        [TestMethod]
        public void TestGetNearestLocation()
        {
            Mock<IWorldCitiesLoader> mockCitiesLoader = new Mock<IWorldCitiesLoader>();
            mockCitiesLoader.Setup(t => t.ReadCities()).Returns(new List<City>()
            {
                new City() { Latitude = 10, Longitude = 10, Name = "testCity1"},
                new City() { Latitude = -5, Longitude = -5, Name = "testCity2"},
                new City() { Latitude = 1, Longitude = 1, Name = "testCity3"}
            });
            WorldCitiesCsvRepository repository = new WorldCitiesCsvRepository(mockCitiesLoader.Object);
            var cities = repository.GetCitiesNearLocation(new GeoCoordinate(0, 0), 2);
            Assert.AreEqual(cities.Count(),2);
            Assert.IsTrue(cities.Contains("testCity2"));
            Assert.IsTrue(cities.Contains("testCity3"));

        }
    }
}
