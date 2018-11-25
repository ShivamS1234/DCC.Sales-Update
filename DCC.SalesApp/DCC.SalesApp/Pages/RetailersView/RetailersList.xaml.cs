using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages.RetailersView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class RetailersList : ContentPage
    {
        public RetailersList()
        {
            InitializeComponent();
            this.Title = "Retailer's Detail";
           

        }

        
    }
}