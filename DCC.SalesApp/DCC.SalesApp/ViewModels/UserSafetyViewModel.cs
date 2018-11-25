using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using AsNum.XFControls;
using DCC.SalesApp.Models;
using DCC.SalesApp.Pages;
using Xamarin.Forms;

namespace DCC.SalesApp.ViewModels
{
    public class UserSafetyViewModel : BaseViewModel
    {

        public string sRead;
        public string SRead
        {
            get { return sRead; }
            set
            {
                if (value != null)
                {
                    sRead = value;
                    this.OnPropertyChanged("SRead");
                }
            }
        }
        public bool pledgeChk;
        public bool PledgeCheck
        {
            get { return pledgeChk; }
            set
            {
                    pledgeChk = value;
                    this.OnPropertyChanged("PledgeCheck");
            }
        }
        public bool safetyChk;
        public bool SafetyCheck
        {
            get { return safetyChk; }
            set
            {
                safetyChk = value;
                this.OnPropertyChanged("SafetyCheck");
            }
        }
        public RadioItem selectedTravel;
        public RadioItem SelectedTravel
        {
            get { return selectedTravel; }
            set
            {
                if (value != null)
                {
                selectedTravel = value;
                    this.OnPropertyChanged("SelectedTravel");
                }
            }
        }

        public ICommand SubmitCommand { protected set; get; }
        public ICommand NotWorking { protected set; get; }
        public ICommand TapLabel { protected set; get; }
        INavigation navigation;

        public UserSafetyViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            SubmitCommand = new Command(OnSubmit);
            NotWorking = new Command(OnNotWorking);
            TapLabel = new Command(TapLabel_Tapped);
        }

        public IEnumerable<RadioItem> RadioGroupSource { get; }
           = new List<RadioItem>() {
                new RadioItem() {ID = 0, Name = "" },
                new RadioItem() {ID = 1, Name="" },
                new RadioItem() {ID = 2, Name = "" }
           };

        public IEnumerable<RadioItem> RadioGroupSource1 { get; }
            = new List<RadioItem>() {
                new RadioItem() {ID = 0, Name = "" },

            };

        public async void OnSubmit()
        {
            UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);

            try
            {
                bool rd1 = false, rd2 = false, rd3 = false;
                var _id = SelectedTravel;

                int TravelId = _id == null ? 10 : _id.ID;
                int TravelRule = SafetyCheck == true ? 10 : 0;

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
                    await UserDialogs.Instance.AlertAsync("Please select travel option.", "Message!", "OK");
                    return;
                }
                else if (!PledgeCheck)
                {
                    await UserDialogs.Instance.AlertAsync("Please select pledge check box.","Message!" , "OK");
                    return;
                }
                else if ((rd1 || rd2 || rd3) && PledgeCheck)
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
                        oUserSafety.StartReading = SRead == "" ? 0 : Convert.ToDecimal(SRead);

                        if (App.Database.SaveUserSafety(oUserSafety) > 0)
                            App.Current.MainPage = new Attendance();
                        else
                            await UserDialogs.Instance.AlertAsync("failed to save","Error !!", "OK");

                    }
                    catch (Exception ex)
                    {
                        await UserDialogs.Instance.AlertAsync(ex.Message,"Error !!", "OK");
                    }
                }
            }
            finally
            {
                // We are back on the original context for this method.
               
                UserDialogs.Instance.HideLoading();
            }
        }

        public async void OnNotWorking()
        {
            UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);
            App.Current.MainPage = new MainPage();
            UserDialogs.Instance.HideLoading();
        }

        private void TapLabel_Tapped()
        {
            //if (PledgeCheck)
            //    PledgeCheck = false;
            //else
            //chkPledge.Checked = true;
            PledgeCheck = PledgeCheck ? false  : true;
        }

    }
}