using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardNew : ContentPage
    {
        public DashboardNew()
        {
            InitializeComponent();
            this.Title = "Dashboard";
            //this.BackgroundColor=""
            
        }

        private void btnSync_Clicked(object sender, EventArgs e)
        {

        }
    }
}