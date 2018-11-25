using System;
using System.Collections.Generic;
using DCC.SalesApp.ViewModels.CommonVM;
using Default;
using Xamarin.Forms;

namespace DCC.SalesApp.Pages.Master
{
    public partial class AddressListPage : ContentPage
    {
        public AddressListPage()
        {
            InitializeComponent();
            BindingContext = new AddressVM(Navigation);
        }

        //public AddressListPage(Retailers retailer)
        //{
        //    InitializeComponent();
        //    BindingContext = new AddressVM(Navigation);
        //}
    }
}
