using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DCC.SalesApp.Models;
using DevExpress.Mobile.DataGrid.Theme;
using DevExpress.Mobile.DataGrid;
using Default;
using Xamarin.Forms.Internals;
using DCC.SalesApp.ViewModels;
using DCC.SalesApp.CustomRenderers;

namespace DCC.SalesApp.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Customers : ContentPage
    {
        Default.Retailers oRetailer;
        CustomerViewModel viewModel;
        Int32 _selectedRetailerID = -1;
        public Customers()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new CustomerViewModel();
            //BindGrid();
            lstRetailers.ItemTapped += OnListItemTapped;
            // viewCustomer.grdCustomer.RowTap += GrdCustomer_RowTap;
        }

        private void OnListItemTapped(object sender, ItemTappedEventArgs e)
        {
            var list = sender as InfiniteListView;
            list.SelectedItem = null;
            var item = e.Item as Customer;
            if (item != null)
            {
                if (_selectedRetailerID != item.ID)
                {
                    _selectedRetailerID = item.ID;
                    Navigation.PushAsync(new CustomerDetails(item.ID) { Title = item.Name });
                }
            }
        }

        private void GrdCustomer_RowTap(object sender, RowTapEventArgs e)
        {
            GridControl gridRow = (GridControl)sender;
            oRetailer = (Retailers)gridRow.SelectedDataObject;
            if (_selectedRetailerID != oRetailer.ID)
            {
                _selectedRetailerID = oRetailer.ID;
                Navigation.PushAsync(new CustomerDetails(oRetailer.ID) { Title = oRetailer.Name });
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _selectedRetailerID = -1;
        }
        public void BindGrid()
        {
            Theme.ApplyGridTheme();
            List<Customer> Retailerslist = App.Database.GetAllCustomerDetails().ToList();
            //lstRetailers.ItemsSource
            //  viewCustomer.grdCustomer.AutoFilterPanelHeight = 30;
            //viewCustomer.grdCustomer.ItemsSource = Retailerslist;
            Theme.ApplyGridTheme();
            ThemeManager.RefreshTheme();
        }
        private bool _canClose = true;
        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            if (_canClose)
            {
                ShowExitDialog();
            }
            return _canClose;
        }
        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Want to go Dashboard Screen?", "Yes", "No");
            if (answer)
            {
                App.Current.MainPage = new MainPage();
                _canClose = false;
                //OnBackButtonPressed;
            }
        }  
    }
}
