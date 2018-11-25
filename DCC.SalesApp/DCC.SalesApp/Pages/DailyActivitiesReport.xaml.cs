using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyActivitiesReport : ContentPage
    {
        public ObservableCollection<Models.DailyActivitiesReport> oDailyActivitiesReport = new ObservableCollection<Models.DailyActivitiesReport>();
        public DailyActivitiesReport()
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
                pkrUser.ItemsSource = App.database.getUsers();
                pkrCustomer.ItemsSource = App.database.getCustomers();
                pkrArea.ItemsSource = App.database.getAreas();
                pkrStatus.ItemsSource = App.database.getActivityStatus();
                oDailyActivitiesReport= App.database.getDailyActivityReport();
                dataGrid.ItemsSource = oDailyActivitiesReport;

                dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription()
                {
                    ColumnName = "AreaCode",
                });
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
            ObservableCollection<Models.DailyActivitiesReport> source = App.database.getDailyActivityReport();
            oDailyActivitiesReport = new ObservableCollection<Models.DailyActivitiesReport>(source.Where(x => x.CreatedDate >= datefrom && x.CreatedDate <= dateto));
            dataGrid.ItemsSource = oDailyActivitiesReport;
        }

        private void pkrFrom_DateSelected(object sender, DateChangedEventArgs e)
        {
            datePickerSelected();
        }

        private void pkrTo_DateSelected(object sender, DateChangedEventArgs e)
        {
            datePickerSelected();
        }

        private void pkrStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.ActivityStatus oActivityStatus = new Default.ActivityStatus();
            oActivityStatus = (Default.ActivityStatus)(sender as Picker).SelectedItem;

            ObservableCollection<Models.DailyActivitiesReport> source = App.database.getDailyActivityReport();
            oDailyActivitiesReport = new ObservableCollection<Models.DailyActivitiesReport>(source.Where(x => x.Status == oActivityStatus.Name));
            dataGrid.ItemsSource = oDailyActivitiesReport;
        }

        private void pkrCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.Retailers oActivityStatus = new Default.Retailers();
            oActivityStatus = (Default.Retailers)(sender as Picker).SelectedItem;

            ObservableCollection<Models.DailyActivitiesReport> source = App.database.getDailyActivityReport();
            oDailyActivitiesReport = new ObservableCollection<Models.DailyActivitiesReport>(source.Where(x => x.Name == oActivityStatus.Name));
            dataGrid.ItemsSource = oDailyActivitiesReport;
        }

        private void pkrUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pkrArea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}