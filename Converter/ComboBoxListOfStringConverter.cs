using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace WPFDynamicFilters.Converter
{
    public class ComboBoxListOfStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> list = null;
            if (value != DependencyProperty.UnsetValue)
            {
                list = value as List<string>;
                if (list == null)
                    return null;
                return new List<object>(list.Cast<object>());
            }

            return list;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<object> list = value as List<object>;
            if (list == null)
                return null;
            return new List<string>(list.Cast<string>());
        }
    }
}
