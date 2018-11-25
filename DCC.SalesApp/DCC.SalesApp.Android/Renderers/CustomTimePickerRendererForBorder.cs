using Android.Widget;
using Xamarin.Forms;
using DCC.SalesApp.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using DCC.SalesApp;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(CustomTimePickerRendererForBorder))]

namespace DCC.SalesApp.Droid.Renderers
{
    class CustomTimePickerRendererForBorder : TimePickerRenderer
    {
        public CustomTimePickerRendererForBorder(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
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
        }
    }
}