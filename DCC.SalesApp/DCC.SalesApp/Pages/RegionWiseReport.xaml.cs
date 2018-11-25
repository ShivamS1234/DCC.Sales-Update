using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;


namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegionWiseReport : ContentPage
    {
        ObservableCollection<Models.RegionWiseReport> oRegionWiseReport = new ObservableCollection<Models.RegionWiseReport>();
        public RegionWiseReport()
        {
            InitializeComponent();
            getData();
        }
        public async void getData()
        {
            Task<bool> task = new Task<bool>(fillControls);
            task.Start();
            bool taskCompleate = await task;

        }
        public bool fillControls()
        {
            try
            {
                pkrCustomers.ItemsSource = App.database.getCustomers();
                pkrUser.ItemsSource = App.database.getUsers();

                oRegionWiseReport =App.database.getRegionWiseReport();
                dataGrid.ItemsSource = oRegionWiseReport;
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x.ToString());
            }

            return true;
        }

        void datePickerSelected()
        {
            DateTime datefrom = pkrFrom.Date;
            DateTime dateto = pkrTo.Date;
            ObservableCollection<Models.RegionWiseReport> source = App.database.getRegionWiseReport();
            oRegionWiseReport = new ObservableCollection<Models.RegionWiseReport>(source.Where(x => x.PostDate >= datefrom && x.PostDate <= dateto));
            dataGrid.ItemsSource = oRegionWiseReport;
        }
        private void pkrFrom_DateSelected(object sender, DateChangedEventArgs e)
        {
            datePickerSelected();
        }

        private void pkrTo_DateSelected(object sender, DateChangedEventArgs e)
        {
            datePickerSelected();
        }

        private void pkrCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.Retailers oRetailer = new Default.Retailers();
            oRetailer = (Default.Retailers)(sender as Picker).SelectedItem;

            ObservableCollection<Models.RegionWiseReport> source = App.database.getRegionWiseReport();
            oRegionWiseReport = new ObservableCollection<Models.RegionWiseReport>(source.Where(x => x.CustCode == oRetailer.Code));
            dataGrid.ItemsSource = oRegionWiseReport;
        }

        private void pkrUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.Users oUser = new Default.Users();
            oUser = (Default.Users)(sender as Picker).SelectedItem;

            ObservableCollection<Models.RegionWiseReport> source = App.database.getRegionWiseReport();
            oRegionWiseReport = new ObservableCollection<Models.RegionWiseReport>(source.Where(x => x.UserId == oUser.ID));
            dataGrid.ItemsSource = oRegionWiseReport;
        }
    }
}