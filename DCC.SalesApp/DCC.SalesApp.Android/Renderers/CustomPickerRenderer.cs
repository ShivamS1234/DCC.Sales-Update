using System.Collections.Generic;
using Xamarin.Forms;
using Android.Widget;
using DCC.SalesApp.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using System.ComponentModel;
using System;
using Android.Content;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace DCC.SalesApp.Droid.Renderers
{
    public class CustomPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        Picker picker; 
        public CustomPickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
           
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }
       
            picker = e.NewElement;
            picker.TextColor  =  Color.FromHex("#036890");
            Control.SetTextSize(Android.Util.ComplexUnitType.FractionParent, 11);
 
 
            //    ApplyStyle(); 
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //if (e.PropertyName == "HeightRequest")
            //    ApplyStyle();
           
        }

   
        private void ApplyStyle()
        {
            //IList<string> scaleNames = picker.Items;
            //Spinner spinner = new Spinner(this.Context);
            //spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
           
            //var scaleAdapter = new ArrayAdapter<string>(this.Context, Android.Resource.Layout.SimpleSpinnerItem, scaleNames);
            //scaleAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //spinner.Adapter = scaleAdapter;
            //base.SetNativeControl(spinner);
            //Control.Background = AddPickerStyles();
  
        }
        //public LayerDrawable AddPickerStyles()
        //{ 
        //    //ShapeDrawable border = new ShapeDrawable();
        //    //border.Paint.Color = Android.Graphics.Color.Rgb(3, 104, 144);
        //    //border.SetIntrinsicWidth(2);
        //    //border.SetPadding(5, 5, 5, 5);
        //    //border.Paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
        //    //Drawable[] layers = { border, GetDrawable() };
        //    //LayerDrawable layerDrawable = new LayerDrawable(layers);
            
        //    //layerDrawable.SetLayerInset(0, 0, 0, 0, 0);
        //    //return layerDrawable;

        //}

        //private BitmapDrawable GetDrawable()
        //{
        //    //int resID = Resource.Drawable.dropdownarrow;
        //    //var drawable = ContextCompat.GetDrawable(this.Context, resID);
        //    //var bitmap = ((BitmapDrawable)drawable).Bitmap;
        //    //var result = new BitmapDrawable(Resources, Android.Graphics.Bitmap.CreateScaledBitmap(bitmap, 25, 25, true));
        //    //result.Gravity = Android.Views.GravityFlags.Right;
        //    //return result;

        //}
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            picker.SelectedItem = spinner.GetItemAtPosition(e.Position);
        }
    }
}