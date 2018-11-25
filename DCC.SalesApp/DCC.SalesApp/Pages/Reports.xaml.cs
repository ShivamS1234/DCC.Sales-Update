using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Default;
using DevExpress.Mobile.DataGrid.Theme;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reports : ContentPage
    {
        Items oItems = new Items();
        public Reports()
        {
            InitializeComponent();
            ThemeManager.ThemeName = Themes.Light;
            BindReport();
        }
        public void BindReport()
        {
            try
            {
                IteamDetail.AutoFilterPanelHeight = 30;
                IteamDetail.ItemsSource = App.database.GetAllItems();
                ThemeManager.RefreshTheme();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error Occured...");
            }
        }

       
 


    }
}



