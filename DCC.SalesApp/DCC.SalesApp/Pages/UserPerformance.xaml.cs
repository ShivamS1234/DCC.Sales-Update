using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Default;
using System.Threading.Tasks;
using System.Linq;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPerformance : ContentPage
    {
        Users currnetUser = App.database.getUser(Convert.ToInt32(Application.Current.Properties["ID"]));
        public UserPerformance()
        {
            InitializeComponent();
            get();
            //  GridControl grid = new GridControl();

            //System.Diagnostics.Debug.WriteLine(UserEOD.pjp_visited);
            //System.Diagnostics.Debug.WriteLine(UserEOD.nonpjp_visited);

        }

        public  void get()
        {
            try
            {
                lblUser.Text = currnetUser.FirstName + " " + currnetUser.MiddleName + " " + currnetUser.LastName;

                int deliveryToday=  App.database.deliveryMadeToday(currnetUser.ID);
                lblDeliveryT.Text = deliveryToday.ToString();

                int OrderToday = App.database.openOrderToday(currnetUser.ID);
                lblOrderT.Text = OrderToday.ToString();

                int OrderYesterday = App.database.openOrderYesterday(currnetUser.ID);
                lblOrderY.Text = OrderToday.ToString();

                var PaymentReceivedToday = App.database.paymentReceivedToday(currnetUser.ID);
                double total = PaymentReceivedToday.Sum(v => Convert.ToDouble(v));
                lblPaymentR.Text = total.ToString();

                int OrderClosedToday = App.database.closedOrderToday(currnetUser.ID);
                lblOrderC.Text = OrderClosedToday.ToString();


                //Models.UserEOD eod = await getEOD();
                //lblVisited.Text=(eod.pjp_visited.Split(',').Length + eod.nonpjp_visited.Split(',').Length).ToString();               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }

        }
        public async Task<Models.UserEOD> getEOD()
        {
            Models.UserEOD UserEOD = await App.PCManager.GetUserEodAsync(currnetUser.ID.ToString()).ConfigureAwait(true);
            return UserEOD;
        }

        public DevExpress.Mobile.Core.Containers.BindingList<Items> Items
        {
            get { return Items; }
            set { Items = setItems(); }
        }

        DevExpress.Mobile.Core.Containers.BindingList<Items> setItems()
        {
            return App.Database.GetAllItems();
        }


    }
}