using System;
using Xamarin.Forms;

namespace DCC.SalesApp
{
    public class CustomEditor:Editor
    {
        public static readonly BindableProperty HasBorderProperty = BindableProperty.Create<CustomEditor, bool>(x => x.HasBorder, true);

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }
      
    }
}
