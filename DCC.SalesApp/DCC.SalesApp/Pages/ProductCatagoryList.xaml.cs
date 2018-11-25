using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCatagoryList : ContentPage
    {
        public ProductCatagoryList()
        {
            InitializeComponent();
            listView.ItemsSource = new ObservableCollection<Default.ITMCategory>(App.database.GetAllProductCatagory());
            //listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            //{
            //    PropertyName = "Name",
            //    KeySelector = (object obj1) =>
            //    {
            //        var item = (obj1 as Default.ITMCategory);
            //        return item.Name[0].ToString();
            //    },

            //});
        }
        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //SalesOrderNew._retailers = (Models.Customer)e.ItemData;
            //QuotationNew._retailers = (Models.Customer)e.ItemData;
            SalesOrderProducts.itmCat = (Default.ITMCategory)e.ItemData;
            QuotationProducts.itmCat = (Default.ITMCategory)e.ItemData;
            Navigation.PopAsync();
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                listView.ItemsSource = new ObservableCollection<Default.ITMCategory>(App.database.GetAllProductCatagory());
            }

            else
            {
                listView.ItemsSource = new ObservableCollection<Default.ITMCategory>(App.database.GetAllProductCatagory().Where(x => x.Name.ToUpper().StartsWith(e.NewTextValue.ToUpper())));
                //list.ItemsSource = tempdata.Where(x => x.Name.StartsWith(e.NewTextValue));
            }
        }
    }
}