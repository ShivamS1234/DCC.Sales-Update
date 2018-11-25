using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductList : ContentPage
    {
        ObservableCollection<Models.SaleProduct> source = new ObservableCollection<Models.SaleProduct>();
        public ProductList()
        {
            InitializeComponent();
            source = App.database.getSaleProducts(0);
            listView.ItemsSource = source;
        }
    }
}