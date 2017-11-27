using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Osk1
{
    public class TagConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                System.Diagnostics.Debug.WriteLine($"tag: {str}");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
