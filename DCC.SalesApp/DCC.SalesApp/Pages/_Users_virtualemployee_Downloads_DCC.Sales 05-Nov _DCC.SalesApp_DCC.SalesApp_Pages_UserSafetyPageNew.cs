sing System;

using Xamarin.Forms;

namespace DCC.SalesApp.Pages
{
    public class UserSafetyPageNew : ContentPage
    {
        public UserSafetyPageNew()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

