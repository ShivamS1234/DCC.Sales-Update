using DCC.SalesApp.Data;
using Xamarin.Forms;
using System;
using DCC.SalesApp.Helpers;
using Plugin.Geolocator;
using DCC.SalesApp.Pages;

namespace DCC.SalesApp
{
    public partial class App : Application
    {
       
        public static DataManager PCManager { get; private set; }
        static public SQLHelper database;
        Page _page;
        public App()
        {
            try
            {
                InitializeComponent();
                MainPage = new LoginPageNew();
                //this.MainPage = GetMainPage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Page GetMainPage()
        {
            return new UserPerformance();
        }

       
        public static SQLHelper Database
        {
            get { if (database == null) { database = new SQLHelper(); } return database; }
        }

        protected override void OnStart()
        {
            PCManager = new DataManager(new RestService());
            checkConnectivity();
            checkGPS();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            checkConnectivity();
            checkGPS();

        }

        protected async void checkConnectivity()
        {
            _page = new Page();
            if (Connectivity_Status.checkConnectivity())
            {
                //await p.DisplayAlert("Message", "Internet Access", "Yes");
            }
            else
            {

                var result = await _page.DisplayAlert("Message", "Enable Internet Access ", "Yes", " ");
                if (result)
                {
                    DependencyService.Get<ISettingsService>().OpenSettings();
                }
            }
        }

        protected async void checkGPS()
        {
            _page = new Page();
            var locator = CrossGeolocator.Current;
            bool b = locator.IsGeolocationEnabled;
            if (b == false)
            {
                var result = await _page.DisplayAlert("Message", "Enable GPS ", "Yes", " ");
                if (result)
                {
                    DependencyService.Get<ICheckGPS>().check_GPS();
                }
            }
        }

    }
}
