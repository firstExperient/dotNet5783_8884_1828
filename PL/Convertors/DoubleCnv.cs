
using System;
using System.Globalization;
using System.Windows.Data;

namespace PL;

internal class DoubleToFixed : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is double)) throw new ArgumentException("value argument must be of type double");
        return System.Math.Round((double)value, 2);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
