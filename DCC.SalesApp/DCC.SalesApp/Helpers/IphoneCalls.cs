namespace DCC.SalesApp.Helpers
{
    public interface IphoneCalls
    {
        void makeCall(string phoneNumber);
    }

    public interface ICalcDistance
    {
        double CalculateDistance(double lat1, double lon1, double lat2, double lon2 );
    }

    public interface IDeviceLocation
    {
        double getLat(Plugin.Geolocator.Abstractions.Position position);
        double getLon(Plugin.Geolocator.Abstractions.Position position);
    }
}
