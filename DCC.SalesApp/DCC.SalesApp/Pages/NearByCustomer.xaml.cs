using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearByCustomer : ContentPage
    {
        Default.Retailers oRetailer;
        Int32 _selectedId = -1;
        public NearByCustomer()
        {
            InitializeComponent();
            try
            {
                viewNearCustomer.grdCustomer.ItemsSource = getDetails().ToList();
                //viewNearCustomer.grdCustomer.ItemSelected += LstRetailers_ItemSelected;
                viewNearCustomer.grdCustomer.RowTap += GrdCustomer_RowTap;
            }
            catch(Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x.StackTrace);
                DisplayAlert("Error", "Distance is not set in Settings", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _selectedId = -1;
        }
        private void GrdCustomer_RowTap(object sender, DevExpress.Mobile.DataGrid.RowTapEventArgs e)
        {
            //throw new NotImplementedException();
            DevExpress.Mobile.DataGrid.GridControl gridControl = (DevExpress.Mobile.DataGrid.GridControl)sender;
            oRetailer = (Default.Retailers)gridControl.SelectedDataObject;
            if (_selectedId != oRetailer.ID)
            {
                _selectedId = oRetailer.ID;
                Navigation.PushAsync(new CustomerDetails(oRetailer.ID) { Title = oRetailer.Name });
            }
        }

        public IEnumerable<Default.Retailers> getDetails()
        {
            
            try
            {
                //if (Device.RuntimePlatform == Device.Android)
                Plugin.Geolocator.Abstractions.Position position=(Plugin.Geolocator.Abstractions.Position) Application.Current.Properties["Position"];
                Models.UserSetting us = App.database.GetUserSetting();
                double deviceLatitude = DependencyService.Get<Helpers.IDeviceLocation>().getLat(position);
                double deviceLongitude= DependencyService.Get<Helpers.IDeviceLocation>().getLon(position);

                List<Models.Customer> retailers = App.database.GetAllCustomerDetails().Where(x=>x.Latitude!=null).ToList();
                var selectedCustomers = new List<Default.Retailers>();
               
                
                foreach (Default.Retailers r in retailers)
                {
                    double distance=DependencyService.Get<Helpers.ICalcDistance>().CalculateDistance(deviceLatitude, deviceLongitude, (double)r.Latitude, (double)r.Longitude);
                    if (distance <= Convert.ToDouble(us.KMforNearBySearch))
                    {
                        selectedCustomers.Add(r);
                        System.Diagnostics.Debug.WriteLine(r.Name);
                    }
                }
                return selectedCustomers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;

            }
        }

        //private void LstRetailers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    oRetailer = (Default.Retailers)e.SelectedItem;
           
        //        Navigation.PushAsync(new CustomerDetails(oRetailer.ID) { Title = oRetailer.Name });
        //}
    }
}
