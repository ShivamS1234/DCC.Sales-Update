using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCustomer : ContentView
    {
        public DevExpress.Mobile.DataGrid.GridControl grdCustomer { get { return lstRetailers; } }
        public ViewCustomer()
        {
            InitializeComponent();

        }
    }
}