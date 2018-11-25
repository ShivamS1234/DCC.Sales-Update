using Plugin.Connectivity;

namespace DCC.SalesApp.Helpers
{
    public static class Connectivity_Status
    {
        internal static bool checkConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public interface ICloseApplication
    {
        void Close_App();
    }
    public interface ICheckGPS
    {
        void check_GPS();
    }
    public interface ISettingsService
    {
        void OpenSettings();
    }

}
