using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductSubCatagoryList : ContentPage
    {
        public ProductSubCatagoryList()
        {
            InitializeComponent();
            
            listView.ItemsSource = new ObservableCollection<Default.ITMSubCat>(App.database.GetAllProductSubCatagory(1));
            
        }
        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {           
            SalesOrderProducts.itmSubCat = (Default.ITMSubCat)e.ItemData;
            QuotationProducts.itmSubCat = (Default.ITMSubCat)e.ItemData;
            Navigation.PopAsync();
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                listView.ItemsSource = new ObservableCollection<Default.ITMSubCat>(App.database.GetAllProductSubCatagory(1));
            }

            else
            {
                listView.ItemsSource = new ObservableCollection<Default.ITMSubCat>(App.database.GetAllProductSubCatagory(1).Where(x => x.Name.ToUpper().StartsWith(e.NewTextValue.ToUpper())));              
            }
        }
    }
}