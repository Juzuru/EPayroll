using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EPayroll.Converters
{
    public class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (((string)value).Equals("Chấp nhận")) return Color.FromHex("#00A08D");
                else if (((string)value).Equals("Từ chối")) return Color.FromHex("#FF0000");
                else return Color.FromHex("#FF9900");
            }
            if (value is int)
            {
                if ((int)value == -1) return Color.FromHex("#FFFFFF");
                else return Color.FromHex("#000000");
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
