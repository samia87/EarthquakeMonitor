using System;
using System.Windows;
using Prism.Mef;
using System.ComponentModel.Composition.Hosting;
using Prism.Logging;
using EarthquakeMonitor.Infrastructure;
using EarthquakeMonitor.Infrastructure.Interfaces;
using EarthquakeMonitor.Modules.Summary;

namespace EarthquakeMonitor
{
    public class EarthquakeMonitorBootstrapper : MefBootstrapper
    {
        public EarthquakeMonitorBootstrapper()
        {
            Application.Current.Exit += new ExitEventHandler(Current_Exit);

        }
        protected override void ConfigureAggregateCatalog()
        {
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(EarthquakeMonitorBootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IEarthquakeFeedService).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(SummaryModule).Assembly));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            Container?.Dispose();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell) this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override Prism.Regions.IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        private readonly EnterpriseLibraryLoggerAdapter _logger = new EnterpriseLibraryLoggerAdapter();

        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }
    }
}