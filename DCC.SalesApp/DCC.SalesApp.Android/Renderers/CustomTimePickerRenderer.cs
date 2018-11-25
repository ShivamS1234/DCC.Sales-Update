using Android.Widget;
using Xamarin.Forms;
using DCC.SalesApp.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(Xamarin.Forms.TimePicker), typeof(CustomTimePickerRenderer))]

namespace DCC.SalesApp.Droid.Renderers
{
    class CustomTimePickerRenderer : TimePickerRenderer
    {
        public CustomTimePickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            Control.TextSize = 12;
        }
    }
}