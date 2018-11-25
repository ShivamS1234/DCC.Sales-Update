using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Default;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using DCC.SalesApp.Helpers;
using DCC.SalesApp.Models;
using System.Linq;

namespace DCC.SalesApp.Views
{

    public partial class UserEODPage : PopupPage
    {
        string isPJp = string.Empty;
        string PJPNpjp = string.Empty;
        int x = 0;
        ObservableCollection<Models.EodOutlets> _outlets = new ObservableCollection<Models.EodOutlets>();
        public UserAttendance OUserAttendance { get; set; }
        public Action<object, EventArgs> CallbackMethod { get; internal set; }
        UserEOD objeo = new UserEOD();

        public UserEODPage(string _isPJp, string _PJPNpjp)
        {
            InitializeComponent();
            isPJp = _isPJp;
            PJPNpjp = _PJPNpjp;
            Bindpjp();
        }

        public void Bindpjp()
        {
            try
            {
                List<string> _selectedoutlets = new List<string>();
                _outlets = App.Database.GetAllEODOutlets(isPJp);

                if (!string.IsNullOrEmpty(PJPNpjp))
                {
                    List<string> Selectedoutlets = PJPNpjp.Split(',').ToList();
                    foreach (var _outlet in _outlets)
                    {
                        if (Selectedoutlets.Contains(_outlet.ID.ToString()))
                            _outlet.Selected = true;
                        else
                            _outlet.Selected = false;
                    }
                }
                lstOutlets.ItemsSource = _outlets;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected override Task OnAppearingAnimationEnd()
        {
            Content.Margin = 40;
            return Content.FadeTo(0.9);
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<string> _selectedoutlets = new List<string>();
                foreach (Models.EodOutlets c in _outlets)
                {
                    if (c.Selected)
                        _selectedoutlets.Add(c.ID.ToString());
                }
                if (isPJp == "Y")
                {
                    Constants.SelectedPJPOutlets = string.Join(",", _selectedoutlets);
                }
                else
                {
                    Constants.SelectedNonPJPOutlets = string.Join(",", _selectedoutlets);
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error !!", ex.Message, "OK");
            }
            await PopupNavigation.PopAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<App>((App)Application.Current, "OnOultletSelected");
        }
        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

    }
}

