using DevExpress.Mobile.DataGrid;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Quotations : ContentPage
    {
        ObservableCollection<Default.Quotations> _quotations = new ObservableCollection<Default.Quotations>();
        Int32 _selectedId = -1;
        public Quotations()
        {
            InitializeComponent();
          
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _selectedId = -1;
            _quotations = App.database.GetAllQuotations();
            _grdQuotations.ItemsSource = _quotations;
            _grdQuotations.AutoFilterPanelHeight = 30;
            _grdQuotations.RowTap += Quotation_RowTap;
            Theme.ApplyGridTheme();
        }
        private void Quotation_RowTap(object sender, RowTapEventArgs e)
        {
            Default.Quotations obj = new Default.Quotations();
            obj = _grdQuotations.SelectedDataObject as Default.Quotations;
            if (_selectedId != obj.ID)
            {
                _selectedId = obj.ID;
                this.Navigation.PushAsync(new QuotationNew(_selectedId) { Title = obj.CustName });
            }
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new QuotationNew(-1) { Title = "New Quotation"});
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