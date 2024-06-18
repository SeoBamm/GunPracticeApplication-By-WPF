using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GunPracticeApplication.Converters
{
    public class AnswerCorrectnessToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string correctness)
            {
                if (correctness == "정답")
                {
                    return new SolidColorBrush(Colors.CornflowerBlue);
                }
                else if (correctness == "오답")
                {
                    return new SolidColorBrush(Colors.IndianRed);
                }
            }
            return new SolidColorBrush(Colors.Black); // Default color if not "정답" or "오답"
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}