using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesOrders : ContentPage
    {
        ObservableCollection<Default.Orders> _quotations = new ObservableCollection<Default.Orders>();
        Int32 _selectedId = -1;
        public SalesOrders()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _selectedId = -1;
            _quotations = App.database.GetAllOrders();
            _grdSalesOrders.ItemsSource = _quotations;
            _grdSalesOrders.AutoFilterPanelHeight = 30;
            _grdSalesOrders.RowTap += _grdSalesOrders_RowTap;
            Theme.ApplyGridTheme();

        }

        private void _grdSalesOrders_RowTap(object sender, DevExpress.Mobile.DataGrid.RowTapEventArgs e)
        {
            Default.Orders obj = new Default.Orders();
            obj = _grdSalesOrders.SelectedDataObject as Default.Orders;
            if (_selectedId != obj.ID)
            {
                _selectedId = obj.ID;
                this.Navigation.PushAsync(new SalesOrderNew(_selectedId) { Title = obj.CustName });
            }
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new SalesOrderNew(-1) { Title = "New Order" });
        }

        private void _grdSalesOrders_SwipeButtonShowing(object sender, DevExpress.Mobile.DataGrid.SwipeButtonShowingEventArgs e)
        {

        }

        private void _grdSalesOrders_SwipeButtonClick(object sender, DevExpress.Mobile.DataGrid.SwipeButtonEventArgs e)
        {

        }
    }
}