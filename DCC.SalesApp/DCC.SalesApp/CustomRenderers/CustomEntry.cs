 
using Xamarin.Forms;

namespace DCC.SalesApp
{
    public class CustomEntry : Entry 
    {
        public static readonly BindableProperty HasBorderProperty = BindableProperty.Create<CustomEntry, bool>(x => x.HasBorder, true);

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }
    }
}
