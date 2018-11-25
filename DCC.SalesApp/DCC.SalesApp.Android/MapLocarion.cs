using Android.Content;
using DCC.SalesApp.Droid;
using DCC.SalesApp.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(MapLocarion))]
namespace DCC.SalesApp.Droid
{
    public class MapLocarion : IMapLocation
    {

        public void GetCurrentLoacation(string latitude, string longitude)
        {
            //throw new NotImplementedException();
            var geoUri = Android.Net.Uri.Parse("geo:" + latitude + "," + longitude + "");
            var mapIntent = new Intent(Intent.ActionView, geoUri);
            Xamarin.Forms.Forms.Context.StartActivity(mapIntent);
        }
    }
}