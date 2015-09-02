using Manager.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Manager.Utilities.Converters
{
    public class DiscountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            OrderItem item = value as OrderItem;
            float discount = ((item.Item.ItemRate - item.AdjustedRate) / (float)item.Item.ItemRate) * 100;
            return discount.ToString("0.00") + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
