using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DCC.SalesApp.CustomRenderers
{
    public class BorderPicker:Picker
    {
        public static readonly BindableProperty HasArrowProperty = BindableProperty.Create<CustomDatePicker, bool>(x => x.HasBorder, false);

        public bool HasArrowBorder
        {
            get { return (bool)GetValue(HasArrowProperty); }
            set { SetValue(HasArrowProperty, value); }
        }

        public BorderPicker()
        {

        }
    }
}
