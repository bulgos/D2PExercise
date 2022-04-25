using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using WeatherTool.MVVM.Model;

namespace WeatherTool.MVVM.Converter
{
    [ValueConversion(typeof(Temperature), typeof(string))]
    public class TemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = (string)value;

            return Temperature.CreateTemperature(stringValue);
        }
    }
}
