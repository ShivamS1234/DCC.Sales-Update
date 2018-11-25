using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.ExternalMaps;
using DCC.SalesApp.Helpers;
using DevExpress.Mobile.DataGrid;
using DCC.SalesApp.Models;
using System.Collections.Generic;
using DevExpress.Mobile.DataGrid.Theme;
using System.Linq;
using Default;
using Plugin.Geolocator;
using Plugin.Messaging;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerDetails : TabbedPage
    {
        int id;
        Int32 _selectedId = -1;
        Default.Retailers oRetailer;
        public CustomerDetails(int _ID)
        {
            id = _ID;
            InitializeComponent();
            Theme.ApplyGridTheme();
       

            Notes_view.grdNotes.AutoFilterPanelHeight = 30;
            Notes_view.grdNotes.RowTap += GrdNotes_RowTap;

            OrderFullFillMent_view.OrderFullFillMent.AutoFilterPanelHeight = 30;
            OrderFullFillMent_view.OrderFullFillMent.RowTap += _grdOrderFulfill_RowTap;

            SetRetailerProperties(_ID);

            BarTextColor = Color.FromHex("#036890");
            BindNotes(oRetailer.Code);
            BindOrderFulFillment(oRetailer.Code);
            GrdTransaction.RowTap += GrdTransaction_RowTap;
            BindTransactions(oRetailer.Code);
        }

      
        public void SetRetailerProperties(int _ID)
        {
            var oVar = App.Database.GetRetailer(_ID);
            oRetailer = (Default.Retailers)oVar;
            lblOwnerName.Text = oRetailer.Name;
            LblOwnerAddress.Text = oRetailer.Address;
            LblOwner.Text = oRetailer.Owner;
            lblOnr_mob.Text = oRetailer.OwnerMobileNo;
            btnphone.Text = oRetailer.OwnerMobileNo;
            LblBalance.Text = oRetailer.Balance.ToString();
            Int32 AreaID = oRetailer.AreaID == null ? 0 : Convert.ToInt32(oRetailer.AreaID);
            Lblarea.Text = App.Database.GetAreaName(AreaID);
        }
        private void btnGetDirection_Clicked(object sender, EventArgs e)
        {
            try
            {
                CrossExternalMaps.Current.NavigateTo(oRetailer.Address, (double)oRetailer.Latitude, (double)oRetailer.Longitude);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void btnUpdateLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    Plugin.Geolocator.Abstractions.Position position = (Plugin.Geolocator.Abstractions.Position)Application.Current.Properties["Position"];
                    App.database.UpdateRetailerLocation(position.Latitude, position.Longitude, oRetailer.ID);
                    
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    var locator =  CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync();
                    App.database.UpdateRetailerLocation(position.Latitude, position.Longitude, oRetailer.ID);
                }
                await DisplayAlert("Message", "Customer's Location Updated.", "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Message", "Error Occured.", "OK");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void btnphone_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    DependencyService.Get<IphoneCalls>().makeCall("+91" + btnphone.Text);
                }
                else
                {
                    var phoneCallTask= CrossMessaging.Current.PhoneDialer;
                    //if (phoneCallTask.CanMakePhoneCall)
                    //{
                        phoneCallTask.MakePhoneCall("+91" + btnphone.Text);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Notes       
        protected void BindNotes(string _BPCode)
        {
            try
            {
                List<NotesInfo> oRNotes = App.Database.GetNotes().Where(X => X.BPCode == _BPCode).ToList();
                Notes_view.grdNotes.AutoFilterPanelHeight = 30;
                Notes_view.grdNotes.ItemsSource = oRNotes;
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

        #endregion
        #region Transaction 
        public void BindTransactions(string CustCode)
        {
            try
            {
                GrdTransaction.AutoFilterPanelHeight = 30;
                List<Invoices> _openInvoices = App.Database.GetOpenInvoices(CustCode).ToList();
                GrdTransaction.ItemsSource = _openInvoices;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void GrdTransaction_RowTap(object sender, RowTapEventArgs e)
        {
            try
            {
                Invoices obj = new Invoices();
                obj = GrdTransaction.SelectedDataObject as Invoices;
                if (_selectedId != obj.ID)
                {
                    _selectedId = obj.ID;
                     this.Navigation.PushAsync(new InvoiceDetail(_selectedId) { Title = obj.CustName });
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region OrderFullfilment


        #region OrderFullfilment 
        protected void BindOrderFulFillment(string CustCode)
        {
            try
            {
                OrderFullFillMent_view.OrderFullFillMent.AutoFilterPanelHeight = 30;
                List<Models.OrderFulfillNew> orderList = App.Database.GetAllorderFullFillNew(CustCode).ToList();
                OrderFullFillMent_view.OrderFullFillMent.ItemsSource = orderList;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        int i = 0;
        private void _grdOrderFulfill_RowTap(object sender, DevExpress.Mobile.DataGrid.RowTapEventArgs e)
        {
            
            if (e.FieldName == "CheckDelivered")
            {
                return;
            } 
            Models.OrderFulfillNew order = (Models.OrderFulfillNew)OrderFullFillMent_view.OrderFullFillMent.SelectedDataObject;
            if (order != null && i == 0)
            {
                Navigation.PushAsync(new OrderFulfillmentProduct(order) { Title = "Order Details" });
                i++;
            }
            else
            {
                i = 0;
                return;
            }
        }
        #endregion
        #endregion
        #region Schemes
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Theme.ApplyGridTheme();
            _selectedId = -1;
            try
            {

                List<Schemes> objSchemes = App.Database.GetAllScheme().ToList();
                _grdSchemes.ItemsSource = objSchemes;
                _grdSchemes.SortMode = GridSortMode.Multiple;
                _grdSchemes.AutoFilterPanelHeight = 30;
                ThemeManager.RefreshTheme();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion        
    }
}