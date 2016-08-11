using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFDynamicFilters
{
    public class FilterTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate IntegerTemplate { get; set; }
        public DataTemplate DropDownTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
                     DependencyObject container)
        {
            var filter = (item as FilterAttribute);
            if (filter == null) return StringTemplate;

            if (!filter.IsDropDown)
            {
                switch (filter.FilterDataType.Name.ToLower())
                {
                    case "int32":
                    case "int64":
                        return IntegerTemplate;
                    case "string":
                        return StringTemplate;
                }
            }
            else
            {
                // display drop down
                switch (filter.DropDownList)
                {
                    case "Country":
                        filter.ObjectDropDownList = GetDropDown.GetCountries().ToList<object>();
                        break;
                }
                return DropDownTemplate;
            }

            return StringTemplate;
        }
    }
}
