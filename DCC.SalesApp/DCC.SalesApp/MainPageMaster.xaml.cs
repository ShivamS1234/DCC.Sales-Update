
using DCC.SalesApp.Menus;
using DCC.SalesApp.Pages;
using DCC.SalesApp.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
            ListView = MenuItemsListView;
            //LblUSerName.Text = "Krishna Kumar";// Application.Current.Properties["FirstName"].ToString();
  
        }

        //class MainPageMasterViewModel : INotifyPropertyChanged
        //{
        //    public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

        //    public MainPageMasterViewModel()
        //    {
        //        MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
        //        {
        //            new MainPageMenuItem { Id = 0, Title = "Dashboard", IconSource = "Dashboard_menu.png", TargetType = typeof(Dashboard)},
        //            new MainPageMenuItem { Id = 1, Title = "Customers", IconSource = "customer_menu.png", TargetType = typeof(Customers)},
        //            new MainPageMenuItem { Id = 2, Title = "Near by Customers", IconSource = "nearbyCustomers.png", TargetType = typeof(NearByCustomer)},
        //            new MainPageMenuItem { Id = 3, Title = "Cash from Customers", IconSource = "cashFromCustomers.png", TargetType = typeof(CashFromCustomer)},
        //            new MainPageMenuItem { Id = 4, Title = "My Account", IconSource = "myaccount.png", TargetType = typeof(MyAccount)},
        //            new MainPageMenuItem { Id = 5, Title = "Stock At Warehouse", IconSource = "stockatWareHouse.png", TargetType = typeof(WarehouseStock)},
        //            new MainPageMenuItem { Id = 6, Title = "Quotations", IconSource = "Quotations_menu.png", TargetType = typeof(Quotations)},
        //            new MainPageMenuItem { Id = 7, Title = "Sales Order", IconSource = "SalesOrder_menu.png", TargetType = typeof(SalesOrders)},
        //            new MainPageMenuItem { Id = 8, Title = "Order Fulfillment", IconSource = "OrderFullfillment.png", TargetType = typeof(OrderFulfillments)},
        //            new MainPageMenuItem { Id = 9, Title = "Notes", IconSource = "notes_menu.png", TargetType = typeof(Notes)},
        //            new MainPageMenuItem { Id = 10, Title = "DSR Report", IconSource = "DSRReport_menu.png", TargetType = typeof(DSRReport)},
        //            new MainPageMenuItem { Id = 11, Title = "Detailed Order Report", IconSource = "DetailsOrderReport_menu.png", TargetType = typeof(DetailOrderReport)},
        //            new MainPageMenuItem { Id = 12, Title = "Region Wise Report", IconSource = "DSRReport_menu.png", TargetType = typeof(RegionWiseReport)},
        //            new MainPageMenuItem { Id = 13, Title = "Daily Activities Report", IconSource = "DailyActivites_menu.png", TargetType = typeof(DailyActivitiesReport)},
        //            new MainPageMenuItem { Id = 14, Title = "User Performance", IconSource = "UserPerfonace_icon.png", TargetType = typeof(UserSettingPage)},
                    
        //            //new MainPageMenuItem { Id = 1, Title = "Retailers", IconSource = "reports.png", TargetType = typeof(Pages.RetailersView.RetailersList)},

        //        });             

        //    }


        //    #region INotifyPropertyChanged Implementation
        //    public event PropertyChangedEventHandler PropertyChanged;
        //    void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //    {
        //        if (PropertyChanged == null)
        //            return;
        //        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //    #endregion
        //}

        
        //private void btnLogOff_Clicked(object sender, System.EventArgs e)
        //{
        //    bool _canClose = true;

        //    App.Current.MainPage = new LoginPageNew();
        //}
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
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
                _canClose = false;
                //OnBackButtonPressed;
            }
        }        

        //private async void btnSetting_Clicked(object sender, System.EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new UserSettingPage());
        //}
    }
}