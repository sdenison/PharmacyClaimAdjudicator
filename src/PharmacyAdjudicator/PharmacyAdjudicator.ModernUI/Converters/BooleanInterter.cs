﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PharmacyAdjudicator.ModernUI.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(bool))
                return !(bool)value;
            if (targetType == typeof(bool?))
                return !(bool?)value;
            throw new NotSupportedException();
        }
    }
}
