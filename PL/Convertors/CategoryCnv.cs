

using System;
using System.Globalization;
using System.Windows.Data;

namespace PL;

internal class CategoryCnv : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is BO.Category)) throw new ArgumentException("value argument must be of type BO.Category");
        switch ((BO.Category)value)
        {
            case BO.Category.MenWatches:
                return "Men Watches";
            case BO.Category.ChildrenWatches:
                return "Children Whatches";
            case BO.Category.WomenWatches:
                return "Women Watches";
            case BO.Category.DivingWatches:
                return "Diving Whatches";
            case BO.Category.SmartWatches:
                return "Smart Whatches";
            default: return "";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (!(value is string)) throw new ArgumentException("value argument must be of type string");
        switch ((string)value)
        {
            case "Men Watches":
                return BO.Category.MenWatches;
            case "Children Whatches":
                return BO.Category.ChildrenWatches;
            case "Women Watches":
                return BO.Category.WomenWatches;
            case "Diving Whatches":
                return BO.Category.DivingWatches;
            case "Smart Whatches":
                return BO.Category.SmartWatches;
            default: return BO.Category.MenWatches;
        }
    }
}
