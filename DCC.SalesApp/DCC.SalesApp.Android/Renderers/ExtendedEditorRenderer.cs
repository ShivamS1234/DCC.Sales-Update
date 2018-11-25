using System;
using DCC.SalesApp;
using DCC.SalesApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(ExtendedEditorRenderer))]
namespace DCC.SalesApp.Droid.Renderers
{
	public class ExtendedEditorRenderer:EditorRenderer
    {
        public ExtendedEditorRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TextSize = 12;
                if (!(e.NewElement as CustomEditor).HasBorder)
                {
                    Control.SetBackground(null);
                }
            }
        }

    }
}
