using DevExpress.Mobile.DataGrid;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewTransaction : ContentView
    {
   

        public GridControl Transactions  { get { return GrdTransaction; } }

        public ViewTransaction( )
        {
            InitializeComponent();
        }
       
        
    }
}