using System;
using System.Collections.Generic;
using AsNum.XFControls;
using DCC.SalesApp.ViewModels;
using Xamarin.Forms;

namespace DCC.SalesApp.Pages
{
    public partial class UserSafetyPageNew : ContentPage
    {
        public UserSafetyPageNew()
        {
            UserSafetyViewModel viewmodel;

            try
            {
                InitializeComponent();
                rd_travelby.PropertyChanged += Rd_travelby_PropertyChanged;
                viewmodel = new UserSafetyViewModel(Navigation);
                BindingContext = viewmodel;
                rd_safety_eq.ShowLabel = true;
                rd_safety_eq.IsVisible = false;
                lbl_safety_eq.IsVisible = false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            rd_travelby.SelectedItem = new RadioItem()
            {
                ID=1,Name=""
            };
        }

            private void Rd_travelby_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();

            RadioGroup x = ((RadioGroup)sender);
            RadioItem r = (RadioItem)x.SelectedItem;
            //var radio = sender as CustomRadioButton;

            try
            {
                if (r.ID == 1)
                {
                    lbl_ans.IsVisible = true;
                    lbl_ans.Text = "Wearing Seat Belt.";
                    rd_safety_eq.IsVisible = true;
                    lbl_safety_eq.IsVisible = true;
                    lbl_SRead.IsVisible = true;
                    txtSRead.IsVisible = true;
                }
                else if (r.ID == 2)
                {
                    lbl_ans.IsVisible = true;
                    lbl_ans.Text = "Wearing Helmet.";
                    rd_safety_eq.IsVisible = true;
                    lbl_safety_eq.IsVisible = true;
                    lbl_SRead.IsVisible = true;
                    txtSRead.IsVisible = true;
                }
                else if (r.ID == 0)
                {
                    lbl_ans.IsVisible = false;
                    lbl_ans.Text = "";
                    rd_safety_eq.IsVisible = false;
                    lbl_safety_eq.IsVisible = false;
                    lbl_SRead.IsVisible = false;
                    txtSRead.IsVisible = false;
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("First Time");
            }

        }
    }
}
