﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Dungecto.Common.Converters
{
    /// <summary><c>enum</c> to <c>bool</c> converter</summary>
    class EnumsToBoolConverter : IValueConverter
    {
        /// <summary>Convert from <c>enum</c> to <c>bool</c> </summary>
        /// <param name="value">Enum value</param>
        /// <param name="parameter">enums values in format (string): "enumZero|enumOne|enumN", when converter returns <c>true</c></param>
        /// <returns> <c>true</c> if <see cref="value"/> is enum and in <see cref="parameter"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return DependencyProperty.UnsetValue; }

            var param = parameter as string;
            if (param == null) { return DependencyProperty.UnsetValue; }

            if (Enum.IsDefined(value.GetType(), value) == false) { return DependencyProperty.UnsetValue; }


            return param.Split('|').Any(x => Enum.Parse(value.GetType(), x).Equals(value));
        }

        /// <summary> Not implemented </summary>        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = parameter as string;
            if (param == null) { return DependencyProperty.UnsetValue; }

            var enumValue = param.Split('|').FirstOrDefault();
            if (enumValue == null) { return DependencyProperty.UnsetValue; }

            return Enum.Parse(targetType, enumValue);
        }
    }
}