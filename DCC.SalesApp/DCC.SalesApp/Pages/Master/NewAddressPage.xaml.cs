using System;
using System.Collections.Generic;
using DCC.SalesApp.ViewModels.CommonVM;
using Xamarin.Forms;

namespace DCC.SalesApp.Pages.Master
{
    public partial class NewAddressPage : ContentPage
    {
        public NewAddressPage()
        {
            InitializeComponent();
            BindingContext = new NewAddressVM(Navigation);
        }
    }
}
