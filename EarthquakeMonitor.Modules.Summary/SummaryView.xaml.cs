using EarthquakeMonitor.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EarthquakeMonitor.Modules.Summary
{
    /// <summary>
    /// Interaction logic for SummaryView.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.SummaryRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SummaryView : UserControl
    {
        public SummaryView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the ViewModel.
        /// </summary>
        /// <remarks>
        /// This set-only property is annotated with the <see cref="ImportAttribute"/> so it is injected by MEF with
        /// the appropriate view model.
        /// </remarks>
        [Import]
        SummaryViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
