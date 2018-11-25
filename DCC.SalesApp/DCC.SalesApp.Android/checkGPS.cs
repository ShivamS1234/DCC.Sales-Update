using Android.Content;
using DCC.SalesApp.Helpers;
using DCC.SalesApp.Droid;
using Xamarin.Forms;
using Android.Locations;

[assembly: Dependency(typeof(checkGPS))]
namespace DCC.SalesApp.Droid
{
    public class checkGPS : ICheckGPS, ISettingsService
    {
        public void check_GPS()
        {
            //throw new NotImplementedException();
            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                Forms.Context.StartActivity(gpsSettingIntent);
            }
        }
        public void OpenSettings()
        {
            //throw new NotImplementedException();
            Forms.Context.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionWifiSettings));
        }
    }
}