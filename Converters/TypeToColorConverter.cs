using BattleShip.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BattleShip.Converters
{
    [ValueConversion(typeof(SquareType), typeof(Brush))]
    public class TypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SquareType type = (SquareType)value;

            switch (type)
            {
                case SquareType.Unknown:
                    return new SolidColorBrush(Colors.LightGray);
                case SquareType.Water:
                    return new SolidColorBrush(Colors.LightBlue);
                case SquareType.Undamaged:
                    return new SolidColorBrush(Colors.Black);
                case SquareType.Damaged:
                    return new SolidColorBrush(Colors.Orange);
                case SquareType.Sunk:
                    return new SolidColorBrush(Colors.Red);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
