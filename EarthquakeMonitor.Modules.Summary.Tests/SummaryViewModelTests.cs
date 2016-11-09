using System;
using System.Collections.Generic;
using EarthquakeMonitor.Infrastructure;
using EarthquakeMonitor.Infrastructure.Interfaces;
using EarthquakeMonitor.Infrastructure.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Events;

namespace EarthquakeMonitor.Modules.Summary.Tests
{
    [TestClass]
    public class SummaryViewModelTests
    {
        [TestMethod]
        public void TestSummaryViewModelConstructor()
        {
            //var eventAggregator = new EventAggregator();
            //var feedService = new Mock<IEarthquakeFeedService>();
            //feedService.Setup(t => t.Status).Returns("test");
            //var viewModel = new SummaryViewModel(feedService.Object, eventAggregator);
            //eventAggregator.GetEvent<EarthquakeActivitiesUpdateEvent>().Publish(new List<EarthquakeActivity>() {new EarthquakeActivity(), new EarthquakeActivity()});
            //Assert.AreEqual(viewModel.EarthquakeActivities.Count,2);
        }
    }
}
