using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Manager.Utilities.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            String param = (String)parameter;
            if (param.Equals("Date"))
                return dateTime.Date.ToString("dd-MMM-yyyy");
            else if (param.Equals("Time"))
                return dateTime.TimeOfDay;
            return "No converter parameter";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
