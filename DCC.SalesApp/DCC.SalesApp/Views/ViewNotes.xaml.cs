using DevExpress.Mobile.DataGrid;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewNotes : ContentView
    {
   

        public GridControl grdNotes { get { return _grdNotes; } }

        public ViewNotes( )
        {
            InitializeComponent();
 
                   
        }
       
        
    }
}