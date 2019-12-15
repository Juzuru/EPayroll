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

            string result = "";
            if (value is int) result = ((int)value).ToString();
            else if (value is long) result = ((long)value).ToString();

            if (result.Equals("0")) return "-";
            if (result.Equals("-1")) return "";
            if (result.Equals("-2")) return "(Giờ)";
            if (result.Equals("-3")) return "(Lần)";

            for (int i = result.Length - 3; i > 0; i -= 3)
            {
                result = result.Insert(i, ".");
            }


            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
