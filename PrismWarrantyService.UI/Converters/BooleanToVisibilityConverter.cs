﻿using System;
using System.Windows;
using System.Windows.Data;

namespace PrismWarrantyService.UI.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool boolean && boolean)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Visibility visibility && visibility == Visibility.Visible)
                return true;

            return false;
        }
    }
}
