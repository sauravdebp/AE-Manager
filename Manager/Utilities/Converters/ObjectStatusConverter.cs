using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Manager.Utilities.Converters
{
    public class ObjectStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IOperationValidity validity = (value as IOperationValidity);
            Operations operation = (Operations)Enum.Parse(typeof(Operations), (String)parameter, true);
            //TODO: Throw exceptions for when operation is unknown or the typeof(value) does not implement IOperationValidity
            if(targetType == typeof(System.Windows.Visibility))
                return IsOperationValid(validity, operation) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            return IsOperationValid(validity, operation);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        bool IsOperationValid(IOperationValidity validity, Operations operation)
        {
            if (validity != null && validity.OperationValidity(operation))
                return true;
            return false;
        }
    }
}
