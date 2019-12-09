using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EPayroll.Converters
{
    public class TextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            if (value is int)
            {
                if ((int)value == -10) return "STT";
                else return (int)value;
            }
            if (value is float)
            {
                if ((float)value == -11) return "Số tiền";
            }

            float number = (float)value;
            float reminder = number % 1000;
            string result = reminder == 0 ? reminder.ToString() + "00" : reminder.ToString();
            number = (number - reminder) / 1000;

            while (number > 999)
            {
                reminder = number % 1000;
                result = reminder.ToString() + "." + result;
                number = (number - reminder) / 1000;
            }
            result = number.ToString() + "." + result;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
