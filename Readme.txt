Application build instructions: 
1. Open "EarthquakeMonitor.sln"
2. Click on Build -> Build Solution via the Visuale studio ide
3. Run application either from IDE or from the bin folder of EarthquakeMonitor project
4. Note the application query the usgs webquery every 30 seconds via IEarthquakeFeedServiceConfig

Application Architecture:
1. EarthquakeMonitor uses prism load the listview module referred to as Summary and contains the bootstrapper and the shellview
2. EarthquakeMonitor.Modules.Summary is the module containing the implementation for the list view region
3. EarthquakeMonitor.Infrastructure contains the EarthquakeMonitorFeedService and it's various dependencies

Design guidelines emphasized:
1. Single responsibility principle was used throughout the application allowing for unit testing and future extensibility
2. Dependency Injection used via MEF

Dependencies and references:
1. CsvHelper nuget package 
2. Prism MEF package
3. Microsoft.AspNet.WebApi.Client package
4. Moq package for mocking in unit tests

Resource files used:
1. worldcities.csv is an embedded resources file inside EarthquakeMonitor.Infrastructure

Notes on further implementations:
1. More unit tests can be placed
2. The projects can be organized into folders if there are going to be future extensions to the application
3. Finding the nearest affected cities algorithm can be optimized, a simple query has been used to implement the logic
4. Exception handling and logging can be improved
5. Configuration can be loaded from app.config
