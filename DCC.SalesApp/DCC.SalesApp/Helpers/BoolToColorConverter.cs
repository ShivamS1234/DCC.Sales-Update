using System;
using System.Globalization;
using Xamarin.Forms;

//namespace DCC.SalesApp.Pages
//{
//    public class BoolToColorConverter:IValueConverter
//    {

//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if((bool)value)
//            {
//                return Color.FromHex("#027ec6");
//            }
//            else
//            {
//                return Color.FromHex("#F4F4F4");
//            }
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
namespace DCC.SalesApp.Pages
{
    public class BoolToColorConverter : IValueConverter
    {
        public string TrueColor
        {
            get;
            set;
        }

        public string FalseColor
        {
            get;
            set;
        }

        public BoolToColorConverter()
        {

        }

        public BoolToColorConverter(string trueColor, string falseColor)
        {

            TrueColor = trueColor;
            FalseColor = falseColor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Color.FromHex(TrueColor);
            }
            else
            {
                return Color.FromHex(FalseColor);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
