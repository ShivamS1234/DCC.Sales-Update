using DevExpress.Mobile.DataGrid;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewOrderFullFillMentNew  : ContentView
    {
   

        public GridControl OrderFullFillMent  { get { return _grdOrderFulfill; } }

        public ViewOrderFullFillMentNew( )
        {
            InitializeComponent();
 
                   
        }
       
        
    }
}