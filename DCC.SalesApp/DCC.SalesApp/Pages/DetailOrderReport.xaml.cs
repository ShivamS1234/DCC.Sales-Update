using Syncfusion.SfDataGrid.XForms;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailOrderReport : ContentPage
    {
        ObservableCollection<Models.DetailOrderReport> _detailOrderReport = new ObservableCollection<Models.DetailOrderReport>();
        public DetailOrderReport()
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
                pkrArea.ItemsSource = App.database.getAreas();
                pkrSalesPerson.ItemsSource = App.database.getUsers();
                pkrCustomers.ItemsSource = App.database.getCustomers();
                _detailOrderReport= App.database.getDetailOrderReport();
                dataGrid.ItemsSource = _detailOrderReport;
                dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription()
                {
                    ColumnName = "SPerson",
                });
                dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription()
                {
                    ColumnName = "AreaCode",
                });
                dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription()
                {
                    ColumnName = "CustName",
                });
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x.ToString());
            }

            return true;
        }

        private void pkrCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.Retailers oRetailer = new Default.Retailers();
            oRetailer = (Default.Retailers)(sender as Picker).SelectedItem;

            ObservableCollection<Models.DetailOrderReport> source = App.database.getDetailOrderReport();
            _detailOrderReport = new ObservableCollection<Models.DetailOrderReport>(source.Where(x=>x.CustName==oRetailer.Name));
            dataGrid.ItemsSource = _detailOrderReport;
        }

        private void pkrSalesPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.Users oUser = new Default.Users();
            oUser = (Default.Users)(sender as Picker).SelectedItem;

            ObservableCollection<Models.DetailOrderReport> source = App.database.getDetailOrderReport();
            _detailOrderReport = new ObservableCollection<Models.DetailOrderReport>(source.Where(x => x.UserID == oUser.ID));
            dataGrid.ItemsSource = _detailOrderReport;
        }

        private void pkrArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.Areas oAreas = new Default.Areas();
            oAreas = (Default.Areas)(sender as Picker).SelectedItem;

            ObservableCollection<Models.DetailOrderReport> source = App.database.getDetailOrderReport();
            _detailOrderReport = new ObservableCollection<Models.DetailOrderReport>(source.Where(x => x.AreaID == oAreas.ID));
            dataGrid.ItemsSource = _detailOrderReport;
        }

        void datePickerSelected()
        {
            DateTime datefrom = pkrFrom.Date;
            DateTime dateto = pkrTo.Date;
            ObservableCollection<Models.DetailOrderReport> source = App.database.getDetailOrderReport();
            _detailOrderReport = new ObservableCollection<Models.DetailOrderReport>(source.Where(x => x.PostDate >= datefrom && x.PostDate <= dateto));
            dataGrid.ItemsSource = _detailOrderReport;
        }

        private void pkrFrom_DateSelected(object sender, DateChangedEventArgs e)
        {
            datePickerSelected();
        }

        private void pkrTo_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}