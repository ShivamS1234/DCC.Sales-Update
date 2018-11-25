using System;
using Android.Content;
using Xamarin.Forms;
using DCC.SalesApp.Droid;
using DCC.SalesApp.Helpers;
using Android.Locations;

[assembly: Dependency(typeof(PhoneCall_Android))]
[assembly: Dependency(typeof(DeviceLocation))]
[assembly: Dependency(typeof(DistanceCalc))]

namespace DCC.SalesApp.Droid
{
    public class PhoneCall_Android : IphoneCalls
    {
        public void makeCall(string phoneNumber)
        {
            try
            {
                var URI = Android.Net.Uri.Parse(String.Format("tel:{0}", phoneNumber));
                var intent = new Intent(Intent.ActionCall, URI);
                Xamarin.Forms.Forms.Context.StartActivity(intent);
                //throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

    }

    public class DistanceCalc : ICalcDistance
    {

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance = 0.0;

            //Calculate radians
            double latitude1Rad = DegreesToRadians(lat1);
            double longitude1Rad = DegreesToRadians(lon1);
            double latititude2Rad = DegreesToRadians(lat2);
            double longitude2Rad = DegreesToRadians(lon2);

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                    Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                    Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            return distance;
        }

        public static double CalculateDistance(params Location[] locations)
        {
            double totalDistance = 0.0;

            for (int i = 0; i < locations.Length - 1; i++)
            {
                Location current = locations[i];
                Location next = locations[i + 1];

                totalDistance += CalculateDistance(current, next);
            }

            return totalDistance;
        }
    }

    public class DeviceLocation : IDeviceLocation
    {
              
        public double getLat(Plugin.Geolocator.Abstractions.Position p)
        {
            Plugin.Geolocator.Abstractions.Position position = p;
            Location l1 = new Location("My Location");
            l1.Latitude = position.Latitude;
            return l1.Latitude;           
            //throw new NotImplementedException();
        }

        public double getLon(Plugin.Geolocator.Abstractions.Position p)
        {
            Plugin.Geolocator.Abstractions.Position position = p;
            Location l1 = new Location("My Location");
            l1.Longitude = position.Longitude;
            return l1.Longitude;
            //throw new NotImplementedException();
        }
    }

}
