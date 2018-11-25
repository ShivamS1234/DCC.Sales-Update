using System;  
using System.ComponentModel;  
using System.Windows.Input;  
using Xamarin.Forms;
using DCC.SalesApp;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DCC.SalesApp.Pages;
using DCC.SalesApp.Menus;
using System.Collections.ObjectModel;

namespace DCC.SalesApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { protected set; get; }
        public ICommand SettingCommand { protected set; get; }
        INavigation navigation;
        public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }
        private bool _canClose = true;
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                this.OnPropertyChanged("UserName");
            }
        }
        private string userID;
        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                this.OnPropertyChanged("UserID");
            }
        }

        public MainPageViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            LogoutCommand = new Command(OnSubmit);
            SettingCommand = new Command(OnSetting);

            MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
          {
                    new MainPageMenuItem { Id = 0, Title = "Dashboard", IconSource = "Dashboard_menu.png", TargetType = typeof(Dashboard)},
                    new MainPageMenuItem { Id = 1, Title = "Customers", IconSource = "customer_menu.png", TargetType = typeof(Customers)},
                    new MainPageMenuItem { Id = 2, Title = "Near by Customers", IconSource = "nearbyCustomers.png", TargetType = typeof(NearByCustomer)},
                    new MainPageMenuItem { Id = 3, Title = "Cash from Customers", IconSource = "cashFromCustomers.png", TargetType = typeof(CashFromCustomer)},
                    new MainPageMenuItem { Id = 4, Title = "My Account", IconSource = "myaccount.png", TargetType = typeof(MyAccount)},
                    new MainPageMenuItem { Id = 5, Title = "Stock At Warehouse", IconSource = "stockatWareHouse.png", TargetType = typeof(WarehouseStock)},
                    new MainPageMenuItem { Id = 6, Title = "Quotations", IconSource = "Quotations_menu.png", TargetType = typeof(Quotations)},
                    new MainPageMenuItem { Id = 7, Title = "Sales Order", IconSource = "SalesOrder_menu.png", TargetType = typeof(SalesOrders)},
                    new MainPageMenuItem { Id = 8, Title = "Order Fulfillment", IconSource = "OrderFullfillment.png", TargetType = typeof(OrderFulfillments)},
                    new MainPageMenuItem { Id = 9, Title = "Notes", IconSource = "notes_menu.png", TargetType = typeof(Notes)},
                    new MainPageMenuItem { Id = 10, Title = "DSR Report", IconSource = "DSRReport_menu.png", TargetType = typeof(DSRReport)},
                    new MainPageMenuItem { Id = 11, Title = "Detailed Order Report", IconSource = "DetailsOrderReport_menu.png", TargetType = typeof(DetailOrderReport)},
                    new MainPageMenuItem { Id = 12, Title = "Region Wise Report", IconSource = "RegionWiseReport_menu.png", TargetType = typeof(RegionWiseReport)},
                    new MainPageMenuItem { Id = 13, Title = "Daily Activities Report", IconSource = "DailyActivites_menu.png", TargetType = typeof(DailyActivitiesReport)},
                    new MainPageMenuItem { Id = 14, Title = "User Performance", IconSource = "UserPerfonace_icon.png", TargetType = typeof(UserSettingPage)},
                    
                    new MainPageMenuItem { Id = 1, Title = "Retailers", IconSource = "reports.png", TargetType = typeof(Pages.RetailersView.RetailersList)},

                });

            setLogInData();
        }
        private void setLogInData()
        {
            UserName = Application.Current.Properties["FirstName"].ToString();
            UserID = Application.Current.Properties["Code"].ToString();
        }

        public async void OnSubmit()
        {
            App.Current.MainPage = new LoginPageNew();
        }

        public async void OnSetting()
        {
            await navigation.PushModalAsync(new UserSettingPage());
        }

    }
}
