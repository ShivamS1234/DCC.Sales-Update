
using Acr.UserDialogs;
using DCC.SalesApp.Data;
using DCC.SalesApp.Models;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;
using Microsoft.Synchronization.ClientServices;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        Int32 _selectedId = -1;

        public Dashboard()
        {
            InitializeComponent();
            GetLocation();
            btnphone1.Clicked += OrderFulfillment_Clicked;
            btnphone2.Clicked += Customers_Clicked;
            btnphone3.Clicked += Items_Clicked;
            btnphone4.Clicked += Orders_Clicked;
            btnphone5.Clicked += Quotations_Clicked;
            btnphone6.Clicked += Notes_Clicked;
            btnphone7.Clicked += Reports_Clicked;
            btnphone8.Clicked += NearByCustomer_Clicked;
            btnphone9.Clicked += sync_Clicked;
            Notes_view.grdNotes.AutoFilterPanelHeight = 30;
            Notes_view.grdNotes.RowTap += GrdNotes_RowTap;

        }
        public async void GetLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync();
            Application.Current.Properties["Position"] = position;
        }
        private void OrderFulfillment_Clicked(object sender, EventArgs e)
        {

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new OrderFulfillments() { Title ="Order Fulfillment" });

        }
        private void Customers_Clicked(object sender, EventArgs e)
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Customers() { Title = "Customers" });

        }
        private void Items_Clicked(object sender, EventArgs e)
        {

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new WarehouseStock() { Title = "Stock at warehouse" });

        }
        private void Orders_Clicked(object sender, EventArgs e)
        {

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SalesOrders() { Title="Orders"});

        }
        private void Quotations_Clicked(object sender, EventArgs e)
        {

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Quotations() { Title = "Quotations" });

        }
        private void Notes_Clicked(object sender, EventArgs e)
        {

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Notes() { Title = "Notes" });

        }
        private void NearByCustomer_Clicked(object sender, EventArgs e)
        {

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new NearByCustomer() { Title = "Near By Customer" });

        }

        private void Reports_Clicked(object sender, EventArgs e)
        {

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Reports() { Title = "Reports" });

        }

        private void sync_Clicked(object sender, EventArgs e)
        {

            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Sync() { Title = "Sync Status" });

        }
        private async void synced_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("syncronization ...", MaskType.Black);
            //var UA = App.Database.GetAllUserAttendances();
            //var USR = App.Database.GetAllUserDetails();
            //var Ret = App.Database.GetAllRetailersDetails();
            //var Saf = App.Database.GetAllUserSafetyDetails();
            btnSync.IsEnabled = false;
            try
            {
                // Can't use ConfigureAwait here.
                await HandleClickAsync();

            }
            finally
            {
                // We are back on the original context for this method.
                btnSync.IsEnabled = true;
                UserDialogs.Instance.HideLoading();
            }

        }
        private async Task HandleClickAsync()
        {

            ContextModelDefault a = /*new ContextModelDefault("1");*/ new ContextModelDefault(Application.Current.Properties["ID"].ToString());

            CacheRefreshStatistics results = await a.Sync();

            if (results.Error != null)
            {
                await DisplayAlert("Sync result", "An error occured during sync session : " + Environment.NewLine + results.Error.Message, "Ok");

            }
            else
            {
                String message = "Sync session completed: " + Environment.NewLine +
                           "Total downloads :" + results.TotalDownloads + Environment.NewLine +
                           "Total uploads   :" + results.TotalUploads + Environment.NewLine +
                           "Total conflicts :" + results.TotalSyncErrors + Environment.NewLine +
                           "Ellapsed Time   :" + results.EndTime.Subtract(results.StartTime).TotalSeconds + " s.";

                await DisplayAlert("Sync result", message, "Ok");

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                Theme.ApplyGridTheme();
                List<NotesInfo> oNotesList = App.Database.GetNotes().Where(x => x.StartDate.Value.Year == System.DateTime.Now.Year &&
                x.StartDate.Value.Month == System.DateTime.Now.Month && x.StartDate.Value.Day == System.DateTime.Now.Day
                ).ToList();
                Notes_view.grdNotes.ItemsSource = oNotesList;
                ThemeManager.RefreshTheme();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void GrdNotes_RowTap(object sender, RowTapEventArgs e)
        {
            NotesInfo obj = new NotesInfo();
            obj = Notes_view.grdNotes.SelectedDataObject as NotesInfo;
            if (_selectedId != obj.ID)
            {
                _selectedId = obj.ID;
                this.Navigation.PushAsync(new NotesDetail(_selectedId) { Title = obj.ActivityName });
            }
        }
    }
}