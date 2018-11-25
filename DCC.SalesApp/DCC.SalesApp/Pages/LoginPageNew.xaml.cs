using System;
using System.Collections.Generic;
using DCC.SalesApp.ViewModels;
using Xamarin.Forms;

namespace DCC.SalesApp.Pages
{
    public partial class LoginPageNew : ContentPage
    {
        LogInPageViewModel viewmodel;

        public LoginPageNew()
        {
            try
            {
                InitializeComponent();
                viewmodel = new LogInPageViewModel(Navigation);
                BindingContext = viewmodel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
