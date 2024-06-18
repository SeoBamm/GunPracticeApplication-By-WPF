using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GunPracticeApplication.Converters
{
    public class IsPassedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string isPassed)
            {
                if (isPassed == "합격")
                {
                    return new SolidColorBrush(Colors.CornflowerBlue);
                }
                else if (isPassed == "불합격")
                {
                    return new SolidColorBrush(Colors.IndianRed);
                }
            }
            return new SolidColorBrush(Colors.Black); // Default color if not "합격" or "불합격"
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}