using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Manager.Utilities.Converters
{
    public class InvertObjectStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObjectStatusConverter converter = new ObjectStatusConverter();
            object convertedVal = converter.Convert(value, targetType, parameter, culture);
            if(convertedVal.GetType() == typeof(System.Windows.Visibility))
            {
                if ((System.Windows.Visibility)convertedVal == System.Windows.Visibility.Visible)
                    return System.Windows.Visibility.Collapsed;
                return System.Windows.Visibility.Visible;
            }
            return !(bool)convertedVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
