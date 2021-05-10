﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BattleShip.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new SolidColorBrush(Colors.Green): new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
