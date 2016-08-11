using System.Windows;

namespace WPFDynamicFilters
{
    /// <summary>
    /// Interaction logic for gridwithfilters.xaml
    /// </summary>
    public partial class GridWithFilters : Window
    {
        public GridWithFilters()
        {
            InitializeComponent();
            this.DataContext = new FilterViewModel();
        }
    }
}
