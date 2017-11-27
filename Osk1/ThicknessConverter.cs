using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Osk1
{


    public class ThicknessConverter : ConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness thickness)
            {
                System.Diagnostics.Debug.WriteLine($"Thickness : {thickness.Left},{thickness.Top},{thickness.Right},{thickness.Bottom}");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
