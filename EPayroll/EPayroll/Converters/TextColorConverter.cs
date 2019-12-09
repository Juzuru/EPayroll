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
            if (((string)value).Equals("Đã trả")) return Color.FromHex("#00A08D");
            else if (((string)value).Equals("Chưa trả")) return Color.FromHex("#FF0000");
            else if (((string)value).Equals("Trạng thái")) return Color.FromHex("#FFFFFF");
            else return Color.FromHex("#FF9900");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
