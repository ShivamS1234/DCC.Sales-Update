using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Syncfusion.SfDataGrid.XForms;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DSRReport : ContentPage
    {
        //public ObservableCollection<Models.User> usrs= new ObservableCollection<Models.User>();
        //public ObservableCollection<Models.Warehouse> wrHouses = new ObservableCollection<Models.Warehouse>();
        //public ObservableCollection<Models.Area> area = new ObservableCollection<Models.Area>();
        public ObservableCollection<Models.DSRReport> oDSR = new ObservableCollection<Models.DSRReport>();
        public DSRReport()
        {
            InitializeComponent();
            getData();
            ListView
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
                //usrs= App.database.getUsers();
                pkrUser.ItemsSource = App.database.getUsers();

                //wrHouses = App.database.getWarehouses();
                pkrWareHouse.ItemsSource = App.database.getWarehouses();

                // area = App.database.getAreas();
                //pkrArea.ItemsSource = App.database.getAreas();
                oDSR = App.database.getOrders();
                dataGrid.ItemsSource = oDSR;

                dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription()
                {
                    ColumnName = "region",
                });
                dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription()
                {
                    ColumnName = "custName",
                });

            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x.ToString());
            }

            return true;
        }

        private void pkrWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.Warehouses oWarehouses = new Default.Warehouses();
            oWarehouses = (Default.Warehouses)(sender as Picker).SelectedItem;

            ObservableCollection<Models.DSRReport> source = App.database.getOrders();
            oDSR = new ObservableCollection<Models.DSRReport>(source.Where(x => x.WhsCode == oWarehouses.ID));
            dataGrid.ItemsSource = oDSR;
        }
        void datePickerSelected()
        {
            DateTime datefrom = pkrFrom.Date;
            DateTime dateto = pkrTo.Date;
            ObservableCollection<Models.DSRReport> source = App.database.getOrders();
            oDSR = new ObservableCollection<Models.DSRReport>(source.Where(x => x.PostDate >= datefrom && x.PostDate <= dateto));
            dataGrid.ItemsSource = oDSR;
        }

        private void pkrFrom_DateSelected(object sender, DateChangedEventArgs e)
        {
            datePickerSelected();
        }        

        private void pkrTo_DateSelected(object sender, DateChangedEventArgs e)
        {
            datePickerSelected();
        }

        private void pkrUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.Users oUsers = new Default.Users();
            oUsers = (Default.Users)(sender as Picker).SelectedItem;

            ObservableCollection<Models.DSRReport> source = App.database.getOrders();
            oDSR = new ObservableCollection<Models.DSRReport>(source.Where(x => x.UserID == oUsers.ID));
            dataGrid.ItemsSource = oDSR;
        }
    }
}