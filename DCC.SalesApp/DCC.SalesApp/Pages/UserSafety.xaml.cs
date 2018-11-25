using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using AsNum.XFControls;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSafety : ContentPage
    {
        public UserSafety()
        {
            InitializeComponent();
            this.BindingContext = this;
            btnSubmit.Clicked += BtnSubmit_Clicked;
            btnskip.Clicked += BtnNotWorking_Clicked;

            rd_travelby.VerticalOptions = LayoutOptions.Start;
            //rd_travelby.ItemsSource = new[] { "Public Transport", "4 Wheeler (Self Driven)", "2 Wheeler (Self Driven)" };

            rd_safety_eq.Text = "Yes";
            rd_safety_eq.ShowLabel = true;
            rd_safety_eq.IsVisible = false;
            rd_travelby.PropertyChanged += Rd_travelby_PropertyChanged;  //ansPicker_CheckedChanged;

            lbl_Pledge_Text.Text = "I hereby Pledge to reach back home safely everyday. I will conduct myself safely and accordance with the travel rules and policies of the country applicable to me. I promise to alert/inform my reporting manager in case I personally violate any safety guidance or witness of any safety violation around me.";
            var tapLabel = new TapGestureRecognizer();
            tapLabel.Tapped += TapLabel_Tapped;
            lbl_Pledge_Text.GestureRecognizers.Add(tapLabel);
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
                    lbl_SRead.IsVisible = true;
                    txtSRead.IsVisible = true;
                }
                else if (r.ID == 2)
                {
                    lbl_ans.IsVisible = true;
                    lbl_ans.Text = "Wearing Helmet.";
                    rd_safety_eq.IsVisible = true;
                    lbl_SRead.IsVisible = true;
                    txtSRead.IsVisible = true;
                }
                else if (r.ID == 0)
                {
                    lbl_ans.IsVisible = false;
                    lbl_ans.Text = "";
                    rd_safety_eq.IsVisible = false;
                    lbl_SRead.IsVisible = false;
                    txtSRead.IsVisible = false;
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("First Time");
            }

        }


        public IEnumerable<RadioItem> RadioGroupSource { get; }
            = new List<RadioItem>() {
                new RadioItem() {ID = 0, Name = "Public Transport" },
                new RadioItem() {ID = 1, Name="4 Wheeler (Self Driven)" },
                new RadioItem() {ID = 2, Name = "2 Wheeler (Self Driven)" }
            };

        public IEnumerable<RadioItem> RadioGroupSource1 { get; }
            = new List<RadioItem>() {
                new RadioItem() {ID = 0, Name = "Yes" },
               
            };

        private void TapLabel_Tapped(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            if (chkPledge.Checked)
                chkPledge.Checked = false;
            else
                chkPledge.Checked = true;
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            bool rd1 = false, rd2 = false, rd3 = false;
            var _id= ((RadioItem)rd_travelby.SelectedItem);

            int TravelId = _id == null ? 10 : _id.ID;
            int TravelRule = rd_safety_eq.Checked==true?10:0;
            
            switch (TravelId)
            {
                case 0:
                    rd1 = true;
                    break;
                case 1:
                    rd2 = true;
                    break;
                case 2:
                    rd3 = true;
                    break;
                default:
                    break;
            }
            if ((!rd1 && !rd2 && !rd3))
            {
                await DisplayAlert("Message!", "Please select travel option.", "OK");
                return;
            }
            else if (!chkPledge.Checked)
            {
                await DisplayAlert("Message!", "Please select pledge check box.", "OK");
                return;
            }
            else if ((rd1 || rd2 || rd3) && chkPledge.Checked)
            {
                try
                {
                    Default.UserSafety oUserSafety = new Default.UserSafety();
                    oUserSafety.ID = App.Database.nextTableID("UserSafety");
                    oUserSafety.UserCode = Application.Current.Properties["Code"].ToString();
                    oUserSafety.CreatedDate = DateTime.Now.Date;
                    oUserSafety.UserId = Convert.ToInt32(Application.Current.Properties["ID"].ToString());

                    oUserSafety.TravelBy = TravelId;
                    oUserSafety.TravelRule = TravelRule;
                    oUserSafety.StartReading = txtSRead.Text == "" ? 0 : Convert.ToDecimal(txtSRead.Text);

                    if (App.Database.SaveUserSafety(oUserSafety) > 0)
                        App.Current.MainPage = new Attendance();
                    else
                        await DisplayAlert("Error !!", "failed to save", "OK");


                    //if (await App.PCManager.AddUserSafety(oUserSafety))
                    //    App.Current.MainPage = new Attendance();
                    //else
                    //{
                    //    await DisplayAlert("Error !!", "failed to save", "OK");
                    //}

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error !!", ex.Message, "OK");
                }
            }
        }


        private void BtnNotWorking_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private void ansPicker_CheckedChanged(object sender, int e)
        {
            RadioItem radio = sender as RadioItem;
            if (radio.ID == 1)
            {
                lbl_ans.IsVisible = true;
                lbl_ans.Text = "Wearing Seat Belt.";
                rd_safety_eq.IsVisible = true;
                lbl_SRead.IsVisible = true;
                txtSRead.IsVisible = true;
            }
            else if (radio.ID == 2)
            {
                lbl_ans.IsVisible = true;
                lbl_ans.Text = "Wearing Helmet.";
                rd_safety_eq.IsVisible = true;
                lbl_SRead.IsVisible = true;
                txtSRead.IsVisible = true;
            }
            else if (radio.ID == 0)
            {
                lbl_ans.IsVisible = false;
                lbl_ans.Text = "";
                rd_safety_eq.IsVisible = false;
                lbl_SRead.IsVisible = false;
                txtSRead.IsVisible = false;
            }

        }

    }
    public class RadioItem
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }
}