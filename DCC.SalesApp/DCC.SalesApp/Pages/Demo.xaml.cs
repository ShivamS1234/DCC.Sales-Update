using Syncfusion.DataSource;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Demo : ContentPage
    {
        public Demo()
        {
           
            InitializeComponent();
            listView.ItemsSource = new ObservableCollection<Models.Customer>( App.database.GetAllCustomerDetails());
            listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "Name",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as Default.Retailers);
                    return item.Name[0].ToString();
                },
                
            });
        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {           
            SalesOrderNew._retailers = (Models.Customer)e.ItemData;
            QuotationNew._retailers = (Models.Customer)e.ItemData;
            Navigation.PopAsync();
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(e.NewTextValue))
            {
                listView.ItemsSource = new ObservableCollection<Models.Customer>(App.database.GetAllCustomerDetails());
            }  
  
          else  
            {
                listView.ItemsSource = new ObservableCollection<Models.Customer>(App.database.GetAllCustomerDetails().Where(x=>x.Name.ToUpper().StartsWith(e.NewTextValue.ToUpper())));
                //list.ItemsSource = tempdata.Where(x => x.Name.StartsWith(e.NewTextValue));
            }
        }
    }
}