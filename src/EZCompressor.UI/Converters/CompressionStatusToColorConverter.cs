using EZCompressor.Core.DataModels;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EZCompressor.UI.Converters
{
    public class CompressionStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((CompressionStatus) value)
            {
                case CompressionStatus.None:
                    return new SolidColorBrush(Colors.Gray);

                case CompressionStatus.Running:
                    return new SolidColorBrush(Colors.Yellow);

                case CompressionStatus.Finished:
                    return new SolidColorBrush(Colors.LightGreen);

                case CompressionStatus.Unfinished:
                    return new SolidColorBrush(Colors.Crimson);

                default:
                    throw new InvalidOperationException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
