using System;
using Xamarin.Forms;

namespace DCC.SalesApp
{
    public class CustomDatePicker:DatePicker
    {
		public static readonly BindableProperty HasBorderProperty = BindableProperty.Create<CustomDatePicker, bool>(x => x.HasBorder, true);

		public bool HasBorder
        {
    		get { return (bool)GetValue(HasBorderProperty); }
			set { SetValue(HasBorderProperty, value); }
		}

        public static readonly BindableProperty HasArrowProperty = BindableProperty.Create<CustomDatePicker, bool>(x => x.HasArrow, true);

        public bool HasArrow
        {
            get { return (bool)GetValue(HasArrowProperty); }
            set { SetValue(HasArrowProperty, value); }
        }

        //public static readonly BindableProperty HasDatePickerProperty = BindableProperty.Create<CustomDatePicker, bool>(x => x.HasDatePicker, false);

        //public bool HasDatePicker
        //{
        //    get { return (bool)GetValue(HasDatePickerProperty); }
        //    set { SetValue(HasDatePickerProperty, value); }
        //}

        public CustomDatePicker()
        {
			
        }
    }
}
