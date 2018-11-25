using DCC.SalesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSettingPage : ContentPage
    {
        UserSetting oUserSetting = null;
        public UserSettingPage()
        {
            InitializeComponent();
            oUserSetting = App.Database.GetUserSetting();
            if (oUserSetting != null)
            {
                if (oUserSetting.ID > 0)
                {
                    txtKM.Text = oUserSetting.KMforNearBySearch.ToString();
                    txtUrl.Text = oUserSetting.WebserverURL;

                    if (oUserSetting.DemoSystem == "Y")
                        DemoSys.IsToggled = true;
                    else
                        DemoSys.IsToggled = false;
                }
            }
            else
            {
                oUserSetting = new UserSetting();
                DemoSys.IsToggled = false;
            }
        }

        private async void btnSubmit_Clicked(object sender, System.EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtKM.Text))
                oUserSetting.KMforNearBySearch = System.Convert.ToDecimal(txtKM.Text);
            oUserSetting.WebserverURL = txtUrl.Text;
            if (DemoSys.IsToggled)
                oUserSetting.DemoSystem = "Y";
            else
                oUserSetting.DemoSystem = "N";
            bool isNew = true;
            if (oUserSetting.ID > 0)
                isNew = false;
            int sucess = App.database.UpdateUserSetting(oUserSetting, isNew);
            if (sucess > 0)
            {
                await DisplayAlert("Confirmation Message", "Settings saved to database.", "OK");
                await Navigation.PopModalAsync();

            }
        }

        private void btnCancel_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}