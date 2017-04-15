using EZCompressor.Core.Compression;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EZCompressor.UI.Converters
{
    public class BooleanToCompressionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool?) value == true && parameter.ToString() == "Lossy")
                return CompressionMode.Lossy;

            else if ((bool?) value == true && parameter.ToString() == "Lossless")
                return CompressionMode.Lossless;

            return null;
        }
    }
}
