using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using DevExpress.Mobile.DataGrid;
using DCC.SalesApp.Models;
using System.Collections.ObjectModel;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WarehouseStock : ContentPage
    {
        Int32 _selectedWhsID = -1;
        ObservableCollection<StockWarehouse> list = new ObservableCollection<StockWarehouse>();
        public WarehouseStock()
        {
            InitializeComponent();          
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                _selectedWhsID = -1;
                   list = App.Database.GetAllWarehouses();
                GrdWareHouse.ItemsSource = list;
                GrdWareHouse.AutoFilterPanelHeight = 30;
                Theme.ApplyGridTheme();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }
        private void GrdWareHouse_RowTap(object sender, RowTapEventArgs e)
        {
            StockWarehouse obj = new StockWarehouse();
            obj = GrdWareHouse.SelectedDataObject as StockWarehouse;
            if (_selectedWhsID != obj.ID)
            {
                _selectedWhsID = obj.ID;
                this.Navigation.PushAsync(new StockItems(_selectedWhsID) { Title = obj.Name });
            }
        }
    }
}