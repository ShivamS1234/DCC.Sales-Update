using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Default;
using System.Collections.Generic;
using AsNum.XFControls;

namespace DCC.SalesApp.Views
{

    public partial class AbsentConfirmation : PopupPage
    {
        Dictionary<Int32,string> n = new Dictionary<int, string>();

        public UserAttendance OUserAttendance { get; set; }
        public AbsentConfirmation(UserAttendance _UserAttendance)
        {
            OUserAttendance = _UserAttendance;
            InitializeComponent();
            if (OUserAttendance.IsAbsent > 0)
            {
                //stkGoals.IsVisible = true; 
                //BindablePicker bp = new BindablePicker();
                //bp.ItemsSource= new[] { "Casual Leave", "Sick Live", "Annaul Leave" }; ;
                RadioGroup bp = new RadioGroup();
                bp.OnImg = "radio1.png";
                bp.OffImg = "radio2.png";
                bp.Orientation = Xamarin.Forms.StackOrientation.Vertical;
                List<RadioItem> lstLeave = createLeaves();
                foreach (var a in lstLeave)
                {
                    n.Add(a.ID, a.Type);
                }
                bp.ItemsSource = n.Values;                
                stkGoals.Children.Add(bp);
                //rd_reason.ItemsSource = new[] { "Casual Leave", "Sick Live", "Annaul Leave" };
                lblAttendanceMessage.Text = "You will be marked as absent for today.";
                lblRemarks.Text = "Reason for absent";
            }
            else
            {
                stkGoals.IsVisible = true;
                //rd_reason.ItemsSource = new[] { "Sales Conference", "Market Visit", "Training", "Retailing", "Head Office", "Promotional Activity" };
                List<Models.Goal> lstGoal = createGoals();
                foreach (Models.Goal g in lstGoal)
                {
                    AsNum.XFControls.CheckBox cb = new AsNum.XFControls.CheckBox();
                    cb.Text = g.Name;
                    cb.ShowLabel = true;
                    cb.OnImg ="ck1.png";
                    cb.OffImg = "ck2.png";
                    //cb.TextColor = Color.FromHex("#036890");
                    stkGoals.Children.Add(cb);
                }

                lblAttendanceMessage.Text = "You will be marked present for today.";
                
                lblGoalReason.Text = "Today's goals";
                lblRemarks.Text = "Agenda Summary";
            }
        }
        public List<Models.Goal> createGoals()
        {
            List<Models.Goal> objList = new List<Models.Goal>();
            Models.Goal goal1 = new Models.Goal() { Id = 1, Name= "Sales Conference" };
            Models.Goal goal2 = new Models.Goal() { Id = 2, Name = "Market Visit" };
            Models.Goal goal3 = new Models.Goal() { Id = 3, Name = "Training" };
            Models.Goal goal4 = new Models.Goal() { Id = 4, Name = "Retailing" };
            Models.Goal goal5 = new Models.Goal() { Id = 5, Name = "Head Office" };
            Models.Goal goal6 = new Models.Goal() { Id = 6, Name = "Promotional Activity" };
            objList.Add(goal1);
            objList.Add(goal2);
            objList.Add(goal3);
            objList.Add(goal4);
            objList.Add(goal5);
            objList.Add(goal6);
            return objList;
        }

        public List<RadioItem> createLeaves()
        {
            List<RadioItem> listLeave = new List<RadioItem>();
            RadioItem leave1 = new RadioItem() { ID=1,Type= "Casual Leave" };
            RadioItem leave2 = new RadioItem() { ID = 2, Type = "Sick Live" };
            RadioItem leave3 = new RadioItem() { ID = 3, Type = "Annaul Leave" };
            listLeave.Add(leave1);
            listLeave.Add(leave2);
            listLeave.Add(leave3);
            return listLeave;
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        protected override Task OnAppearingAnimationEnd()
        {
            Content.Margin = 40;
            return Content.FadeTo(0.9);
        }

        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                OUserAttendance.Id = App.Database.nextTableID("UserAttendance");
                OUserAttendance.UserId = Application.Current.Properties["ID"].ToString() == "" ? 0 : Convert.ToInt32(Application.Current.Properties["ID"].ToString());
                OUserAttendance.UserCode = Application.Current.Properties["Code"].ToString();
           //     OUserAttendance.ReasonId = 0;// rd_reason.SelectedIndex;
                OUserAttendance.Remarks = Remarks.Text;
                OUserAttendance.LoginTime = DateTime.Now;

                if (App.Database.SaveUserAttendance(OUserAttendance) > 0)
                {
                    await PopupNavigation.PopAsync();
                    App.Current.MainPage = new MainPage();
                }
                else
                    await DisplayAlert("Error !!", "failed to save", "OK");


                //if (await App.PCManager.AddUserAttendance(OUserAttendance))
                //{
                //    await PopupNavigation.PopAsync();
                //    App.Current.MainPage = new MainPage();
                //}
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error !!", ex.Message, "OK");
            }


        }
    }

    public class RadioItem
    {
        public string Type { get; set; }
        public int ID { get; set; }
    }

}