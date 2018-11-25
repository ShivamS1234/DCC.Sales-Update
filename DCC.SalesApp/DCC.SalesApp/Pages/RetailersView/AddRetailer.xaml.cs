using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCC.SalesApp.ViewModels.RetailerVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages.RetailersView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRetailer : ContentPage
    {
        #region constructor
        public AddRetailer()
        {
            InitializeComponent();
            BindingContext = new AddRetailerViewModel(Navigation);
        }
        #endregion

        #region event_method

        #endregion
    }
}