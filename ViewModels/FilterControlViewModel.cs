using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace WPFDynamicFilters
{
    public class FilterControlViewModel :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private List<FilterAttribute> filterDetails;

        private string selectedFilter;
        public string SelectedFilter
        {
            get
            {
                return selectedFilter;
            }
            set
            {
                var oldValue = selectedFilter;
                selectedFilter = value;
                ExecuteFilterName(value);
                if (selectedFilter != "Select")
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => SelectedFilter = FilterNames[0]),
                                                     DispatcherPriority.ApplicationIdle);

                NotifyPropertyChanged("SelectedFilter");
            }
        }

        public ICommand FilterNameCommand { get; set; }
        public ICommand RemoveFilterCommand { get; set; }

        // public List<FilterAttribute> FilterDetails { get; set; }

        public List<FilterAttribute> FilterDetails
        {
            get
            {
                return filterDetails;
            }
            set
            {
                filterDetails = value;

                FilterNames = new ObservableCollection<string>(filterDetails.Select(f => f.FilterLabel).ToList());
                FilterNames.Insert(0, "Select");
                SelectedFilter = FilterNames[0];
            }
        }

        public ObservableCollection<FilterAttribute> SelectedFilters { get; set; }

        public ObservableCollection<string> FilterNames { get; set; }

        public string Count
        {
            get
            {
                return FilterDetails.Count.ToString();
            }
        }

        public FilterControlViewModel()
        {
            FilterDetails = new List<FilterAttribute>();
            SelectedFilters = new ObservableCollection<FilterAttribute>();
            FilterNameCommand = new DelegateCommand<string>(ExecuteFilterName);
            RemoveFilterCommand = new DelegateCommand<string>(ExecuteRemoveFilterName);
        }

        private void ExecuteRemoveFilterName(string filterKey)
        {
            if (SelectedFilters.Any(f => f.FilterKey == filterKey))
            {
                var elementToBeRemoved = SelectedFilters.Where(f => f.FilterKey == filterKey).First();
                SelectedFilters.Remove(elementToBeRemoved);
            }
        }

        private void ExecuteFilterName(string filterKey)
        {
            if (filterKey == FilterNames[0]) return;
            if (!SelectedFilters.Any(f => f.FilterKey == filterKey))
            {
                SelectedFilters.Add(filterDetails.Where(f => f.FilterKey == filterKey).First());
            }
        }
    }
}
