

using System;
using System.Globalization;
using System.Windows.Data;

namespace PL;

internal class StateToVisibleCnv : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is State)) throw new ArgumentException("value argument must be of type State");
        if ((State)value == State.View) return true;
        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
