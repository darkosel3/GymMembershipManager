using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GymMembershipManager.Services
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isPaid = value is bool b && b;
            return isPaid ? Brushes.MediumSeaGreen : Brushes.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}