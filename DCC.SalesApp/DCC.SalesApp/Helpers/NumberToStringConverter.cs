
using System;
using System.Globalization;
using Xamarin.Forms;

namespace DCC.SalesApp.Pages
{
    public class NumberToStringYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(int.Parse(value.ToString()) == 1)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NumberToStringPriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "H")
            {
                return "High";
            }
            else if (value.ToString() == "M")
            {
                return "Medium";
            }
            else
            {
                return "Low";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}