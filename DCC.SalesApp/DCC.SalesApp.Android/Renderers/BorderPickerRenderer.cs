using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DCC.SalesApp.Droid.Renderers;
using DCC.SalesApp.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(BorderPicker), typeof(BorderPickerRenderer))]
namespace DCC.SalesApp.Droid.Renderers
{
    public class BorderPickerRenderer:PickerRenderer
    {
        public BorderPickerRenderer(Context context) : base(context)
        {

        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
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
                if ((e.NewElement as BorderPicker).HasArrowBorder)
                {
                    Control.TextSize = 12;
                    Control.SetTextColor(Android.Graphics.Color.Black);
                    Control.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F4F4F4"));
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.style_PickerControl, 0);
                }
                
            }
        }
    }
}