using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WPFDynamicFilters
{
    public class FilterViewModel
    {
        public ICommand CheckFiltersCommand { get; set; }
        public FilterViewModel()
        {
            CheckFiltersCommand = new DelegateCommand(GetFilters);
            GridSource = new List<GridModel>();
            GridSource.Add(new GridModel() { Id = 1, Name = "Name1", Country = "Denmark" });
            GridSource.Add(new GridModel() { Id = 2, Name = "Name2", Country = "India" });
            GridSource.Add(new GridModel() { Id = 3, Name = "Name3", Country = "Australia" });
            GridSource.Add(new GridModel() { Id = 4, Name = "Name4", Country = "India" });
            GridSource.Add(new GridModel() { Id = 5, Name = "Name5", Country = "Australia" });
            GridSource.Add(new GridModel() { Id = 6, Name = "Name6", Country = "Hongkong" });

            FilterControlViewModel = new FilterControlViewModel();
            FilterControlViewModel.FilterDetails = new List<FilterAttribute>();

            List<FilterAttribute> filterDetails = new List<FilterAttribute>();
            foreach (var property in typeof(GridModel).GetProperties())
            {
                if (property.GetCustomAttributes(true).Where(attr => attr.GetType() == typeof(FilterAttribute)).Any())
                {
                    var attribute = (FilterAttribute)property.GetCustomAttributes(true).Where(attr => attr.GetType() == typeof(FilterAttribute)).First();
                    filterDetails.Add(attribute);
                }
            }
            FilterControlViewModel.FilterDetails = filterDetails;
        }

        private void GetFilters()
        {
            FilterCollection = new Dictionary<string, object>();
            foreach (var filter in FilterControlViewModel.SelectedFilters)
            {
                if (filter.IsDropDown)
                {
                    if (filter.FilterValue != null)
                        FilterCollection.Add(filter.FilterKey, filter.FilterValue.GetType().GetProperty("Id").GetValue(filter.FilterValue));
                }
                else
                {
                    FilterCollection.Add(filter.FilterKey, filter.FilterValue);
                }
            }

            MessageBox.Show(string.Join(", ", FilterCollection.Select(m => m.Key + ":" + Convert.ToString(m.Value)).ToArray()));
        }

        public List<GridModel> GridSource { get; set; }

        public Dictionary<string, object> FilterCollection { get; set; }

        public FilterControlViewModel FilterControlViewModel { get; set; }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _action;

        public DelegateCommand(Action<T> action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GridModel
    {
        [Filter(FilterLabel = "Id",
            FilterKey = "Id",
            IsDropDown = false,
            FilterDataType = typeof(int))]
        public int Id { get; set; }
        [Filter(FilterLabel = "Name",
            FilterKey = "Name",
            IsDropDown = false,
            FilterDataType = typeof(string))]
        public string Name { get; set; }
        [Filter(FilterLabel = "Country",
            FilterKey = "Country",
            IsDropDown = true,
            FilterDataType = typeof(int),
            DropDownList = "Country")]
        public string Country { get; set; }

        [Filter(FilterLabel = "Address",
            FilterKey = "Address",
            IsDropDown = false,
            FilterDataType = typeof(string))]
        public string Address { get; set; }
    }

    public class FilterAttribute : Attribute
    {
        public FilterAttribute() { }
        public string FilterLabel { get; set; }
        public object FilterValue { get; set; }
        public string FilterKey { get; set; }
        public Type FilterDataType { get; set; }
        public bool IsDropDown { get; set; }
        public string DropDownList { get; set; }
        public List<object> ObjectDropDownList { get; set; }
    }
}
