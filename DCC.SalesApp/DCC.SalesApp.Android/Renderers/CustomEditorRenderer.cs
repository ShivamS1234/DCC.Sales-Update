using Android.Content;
using DCC.SalesApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Editor), typeof(CustomEditorRenderer))]
namespace DCC.SalesApp.Droid.Renderers
{
    class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);
            Control.Background = this.Resources.GetDrawable(Resource.Drawable.RoundedCornerEntry);
            Control.SetTextColor(Android.Graphics.Color.Rgb(3, 104, 144));
            Control.SetTextSize(Android.Util.ComplexUnitType.FractionParent, 11);
        }
    }
}