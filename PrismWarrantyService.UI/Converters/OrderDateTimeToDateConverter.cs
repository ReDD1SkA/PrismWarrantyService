using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PrismWarrantyService.UI.Converters
{
    public class OrderDateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime?) value)?.ToString("dd.MM.yyyy") ?? "не завершен";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}