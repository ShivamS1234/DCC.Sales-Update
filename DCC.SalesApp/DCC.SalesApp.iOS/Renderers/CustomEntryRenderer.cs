using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using DCC.SalesApp;
using DCC.SalesApp.iOS;
using DCC.SalesApp.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace DCC.SalesApp.iOS.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }

        }

    }
}