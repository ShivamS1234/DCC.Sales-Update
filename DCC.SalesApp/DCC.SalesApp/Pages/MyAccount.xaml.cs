using DCC.SalesApp.Helpers;
using DCC.SalesApp.Models;
using Default;
using Plugin.Geolocator;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;
using DCC.SalesApp.Views;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccount : TabbedPage
    {
        Users oUser;
        UserEOD oEOD = new UserEOD();
        EndOfDay objeod = new EndOfDay();
        //    UserEOD objEod = new UserEOD();
        Int32 _selectedId = -1;
        public MyAccount()
        {
            InitializeComponent();

            var tapImage_PJP = new TapGestureRecognizer();
            tapImage_PJP.Tapped += BtnVistited_Clicked;
            ImgPJP.GestureRecognizers.Add(tapImage_PJP);

            var tapImage_NPJP = new TapGestureRecognizer();
            tapImage_NPJP.Tapped += BtnNVistited_Clicked;
            ImgNPJP.GestureRecognizers.Add(tapImage_NPJP);
            UserDetails();
            //Transactions         
            Transactions_view.Transactions.RowTap += GrdTransaction_RowTap;
            BindTransactions();

            MessagingCenter.Subscribe<App>((App)Application.Current, "OnOultletSelected", (sender) =>
            {
                UpdateSelectecOutlets();
            });

        }
        public void UserDetails()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();

            BarTextColor = Xamarin.Forms.Color.FromHex("#036890");
            int _userId =  Convert.ToInt32(Application.Current.Properties["ID"].ToString() == "" ? "0" : Application.Current.Properties["ID"].ToString());
            if (_userId > 0)
            {
                try
                {
                    oUser = App.Database.GetUserDetails(_userId);
                    UserAttendance oUserAttendance = App.Database.GetUserLastLogin(_userId);

                    double _totalDays = (DateTime.Now - oUserAttendance.LoginTime).TotalDays;

                    if (_totalDays >= 1.0 || _totalDays <= -1)
                    {
                        LblAttend.Text = "Marked";
                        LblWorkingStatus.Text = "Not Working Today";
                        LblStatus.Text = "Absent";
                    }
                    else
                    {
                        LblAttend.Text = "Marked";
                        LblWorkingStatus.Text = "Working Today";
                        LblStatus.Text = "Present";
                    }
                    LblPhone.Text = oUser.MobileNo;
                    LblLogTime.Text = oUserAttendance.LoginTime.ToString();
                    LblAddress.Text = oUser.Address;

                    LblUserName.Text = oUser.FirstName + " " + ((string.IsNullOrEmpty(oUser.LastName)) ? "" : oUser.LastName);
                    LblEmail.Text = oUser.Email;

                    int DesignationId = oUser.DesignationId == null ? 0 : Convert.ToInt32(oUser.DesignationId);
                    if (DesignationId > 0)
                        LblDesignation.Text = App.Database.GetDesignationName(DesignationId);
                    Att_Image.Source = ImageSource.FromStream(() => new MemoryStream(oUser.ProfileImg));
                }
                catch(Exception ex)
                {

                }
            }
            else
                App.Current.MainPage = new LoginPageNew();
        }
        private async void location_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();
            DependencyService.Get<IMapLocation>().GetCurrentLoacation(position.Latitude.ToString(), position.Longitude.ToString());
        }

        public void BtnEdit_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new UpdateAccount(oUser) { Title = "Edit Info" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Transaction 
        public void BindTransactions()
        {
            try
            {
                Theme.ApplyGridTheme();
                Transactions_view.Transactions.AutoFilterPanelHeight = 30;
                List<Models.Transaction> _transactions = App.Database.GetTransactions(1).ToList();// Application.Current.Properties["ID"]
                Transactions_view.Transactions.ItemsSource = _transactions;
                ThemeManager.RefreshTheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _selectedId = -1;
        }
        private void GrdTransaction_RowTap(object sender, RowTapEventArgs e)
        {

            try
            {

                Models.Transaction obj = new Models.Transaction();

                obj = Transactions_view.Transactions.SelectedDataObject as Models.Transaction;

                if (_selectedId != obj.SalesOrder)
                {
                    _selectedId = obj.SalesOrder;

                    if (obj.TransactionType == "Order")
                    {
                        this.Navigation.PushAsync(new SalesOrderNew(_selectedId) { Title = obj.CustName });
                    }
                    else if (obj.TransactionType == "Notes")
                    {
                        this.Navigation.PushAsync(new NotesDetail(_selectedId) { Title = obj.CustName });
                    }
                    else if (obj.TransactionType == "Quotation")
                    {
                        this.Navigation.PushAsync(new QuotationNew(_selectedId) { Title = obj.CustName });
                    }

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        private async void EOD_Appearing(object sender, EventArgs e)
        {

            try
            {
                int userId = (int)Application.Current.Properties["ID"];
                var reuserEod = await App.PCManager.GetUserEodAsync(userId.ToString()).ConfigureAwait(true);
                if (reuserEod != null)
                {
                    TcallMade.Text = reuserEod.call_made;
                    TPC.Text = reuserEod.productive_call;
                    oEOD.nonpjp_visited = reuserEod.nonpjp_visited;
                    oEOD.pjp_visited = reuserEod.pjp_visited;
                    oEOD.userid = reuserEod.userid;
                    oEOD.eod_date = reuserEod.eod_date;
                }
                else
                {
                    UpdateSelectecOutlets();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateSelectecOutlets()
        {
            Int32 _PJPTOutlets = App.Database.GetAllOutlets().Where(x => x.IsPJP == "Y").Count();
            Int32 _PJPTVisited = Constants.SelectedPJPOutlets == "" ? 0 : Constants.SelectedPJPOutlets.Split(',').Count();
            Int32 _PJPTNvisited = _PJPTOutlets - _PJPTVisited;

            Int32 _NPJPTOutlets = App.Database.GetAllOutlets().Where(x => x.IsPJP == "N").Count(); //3
            Int32 _NPJPTVisited = Constants.SelectedNonPJPOutlets == "" ? 0 : Constants.SelectedNonPJPOutlets.Split(',').Count(); // 4
            Int32 _NPJPTNvisited = _NPJPTOutlets - _NPJPTVisited;

            PJPTOutlets.Text = _PJPTOutlets.ToString(); //1       
            PJPTVisited.Text = _PJPTVisited.ToString(); //2
            PJPTNvisited.Text = _PJPTNvisited.ToString(); // 1-2 = X

            NPJPTOutlets.Text = _NPJPTOutlets.ToString(); //3
            NPJPTVisited.Text = _NPJPTVisited.ToString(); // 4
            NPJPTNvisited.Text = _NPJPTNvisited.ToString(); ; // 3-4 = Y

            TotalOutlets.Text = (_PJPTOutlets + _NPJPTOutlets).ToString();

            AddOutlets.Text = App.Database.GetAllOutlets().Where(x => x.CreatedDate.Value.Year == System.DateTime.Now.Year &&
                x.CreatedDate.Value.Month == System.DateTime.Now.Month && x.CreatedDate.Value.Day == System.DateTime.Now.Day).Count().ToString();
            TotalVisited.Text = (_PJPTVisited + _NPJPTVisited).ToString(); // X+Y
            Productivity.Text = Convert.ToString(objeod.AddOutlets + objeod.NotVisited);
        }

        async void BtnVistited_Clicked(object sender, EventArgs e)
        {

            var page = new UserEODPage("Y", oEOD.pjp_visited);
            try
            {
                await Navigation.PushPopupAsync(page);
            }
            catch (Exception ex)
            {

            }

        }
        public void OnPopupUnitsCallback(object sender, System.EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Closed", "Successfully Added to your favorite.", "Ok");
        }



        private async void BtnNVistited_Clicked(object sender, EventArgs e)
        {
            var page = new UserEODPage("N", oEOD.nonpjp_visited);
            try
            {
                await Navigation.PushPopupAsync(page);
            }
            catch (Exception ex)
            {

            }
        }

        private async void Btnsave_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);
            Btnsave.IsEnabled = false;
            try
            {
                await HandleClickAsync();
            }
            finally
            {
                // We are back on the original context for this method.
                Btnsave.IsEnabled = true;
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task HandleClickAsync()
        {
            try
            {
                UserEOD oUserEOD = new UserEOD();
                oUserEOD.eod_date = DateTime.Now.Date;
                oUserEOD.userid =  Convert.ToInt32(Application.Current.Properties["ID"].ToString());
                oUserEOD.call_made = TcallMade.Text;
                oUserEOD.productive_call = TPC.Text;
                oUserEOD.pjp_visited = Constants.SelectedPJPOutlets;
                oUserEOD.nonpjp_visited = Constants.SelectedNonPJPOutlets;
                var results = await App.PCManager.AddUserEOD(oUserEOD).ConfigureAwait(true);
                if (results)
                    await DisplayAlert("Success", "EOD details are saved successfully", "Ok");
                else
                    await DisplayAlert("Failure", "EOD details are not saved. please try again", "Ok");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
