using System.Collections.Generic;

namespace WPFDynamicFilters
{
    public class FilterControlViewModel
    {
        public List<FilterAttribute> FilterDetails { get; set; }

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
        }
    }
}
