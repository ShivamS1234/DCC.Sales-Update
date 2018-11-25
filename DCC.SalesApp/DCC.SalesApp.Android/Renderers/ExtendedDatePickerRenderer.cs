using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using DCC.SalesApp;
using DCC.SalesApp.CustomRenderers;
using DCC.SalesApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomDatePicker),typeof(ExtendedDatePickerRenderer))]
namespace DCC.SalesApp.Droid.Renderers
{
    public class ExtendedDatePickerRenderer:DatePickerRenderer
    {
        public ExtendedDatePickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TextSize = 12;
                if (!(e.NewElement as CustomDatePicker).HasBorder)
                {
                    Control.SetBackground(null);
                }
                if ((e.NewElement as CustomDatePicker).HasArrow)
                {
                    GradientDrawable shape = new GradientDrawable();
                    shape.SetShape(ShapeType.Rectangle);
                    shape.SetCornerRadius(0);
                    shape.SetStroke(3, Android.Graphics.Color.LightGray);
                    this.Control.SetBackground(shape); 
                    Control.TextSize = 12;
                    Control.SetTextColor(Android.Graphics.Color.Black);
                    Control.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F4F4F4"));
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.style_PickerControl, 0);

                }
                else
                {
                    GradientDrawable shape = new GradientDrawable();
                    shape.SetShape(ShapeType.Rectangle);
                    shape.SetCornerRadius(0);
                    shape.SetStroke(3, Android.Graphics.Color.LightGray);
                    this.Control.SetBackground(shape);
                    Control.TextSize = 12;
                    Control.SetTextColor(Android.Graphics.Color.Black);
                    Control.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F4F4F4"));
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.style_DatePicker, 0);
                }
            }
        }
    }
}
