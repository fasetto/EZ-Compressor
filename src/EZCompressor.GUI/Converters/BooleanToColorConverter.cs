using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EZCompressor.UI.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value)
                return new SolidColorBrush(Colors.LightGreen);

            return new SolidColorBrush(Colors.Crimson);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
