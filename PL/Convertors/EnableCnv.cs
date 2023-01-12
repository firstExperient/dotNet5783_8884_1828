using System;
using System.Globalization;
using System.Windows.Data;

namespace PL;
internal class AddToEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is State)) throw new ArgumentException("value argument must be of type State");
        if ((State)value == State.Add) return true;
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class UpdateToEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is State)) throw new ArgumentException("value argument must be of type State");
        if ((State)value == State.Update) return true;
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class ConfirmedToEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is BO.OrderStatus)) throw new ArgumentException("value argument must be of type BO.OrderStatus");
        if ((BO.OrderStatus)value == BO.OrderStatus.Confirmed) return true;
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

internal class ShipedToEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is BO.OrderStatus)) throw new ArgumentException("value argument must be of type BO.OrderStatus");
        if ((BO.OrderStatus)value == BO.OrderStatus.Shipped) return true;
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

 
internal class InCartToEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is int)) throw new ArgumentException("value argument must be of type int");
        if ((int)value > 0) return true;
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}