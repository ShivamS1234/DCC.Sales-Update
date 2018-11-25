using Acr.UserDialogs;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using DCC.SalesApp.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using DCC.SalesApp.Data;
using Microsoft.Synchronization.ClientServices;
using System.ComponentModel;
using System.Collections.Generic;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sync : ContentPage
    {

        public ListView ListView;
        SyncTableViewModel OClass = null;
        Dictionary<string, bool> updatedList = new Dictionary<string, bool>();
        public Sync()
        {
            InitializeComponent();
            UserDialogs.Instance.ShowLoading("loading ...", MaskType.Black);
            OClass = new SyncTableViewModel();
            BindingContext = OClass;
            ListView = synclistView;

       
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loading();
        }

        private void loading()
        {
            //base.OnAppearing();
            try
            {
                var count = Task.Run(async () => { return await HandleAppearingAsync("1"); }).Result;
                foreach (var data in OClass.SyncedTablelist)
                {
                    try
                    {
                        if (count.Find(x => x.TableName == data.TableName).TableRows == data.TableRows)
                        {
                            updatedList.Add(data.TableName, true);
                            data.SyncStatus = true;
                        }
                        else
                            updatedList.Add(data.TableName, false);
                    }
                    finally
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

        }
        private async Task<List<Models.SyncedTables>> HandleAppearingAsync(string _tableName)
        {
            var count = await App.PCManager.RefreshTableRowsAsync(_tableName).ConfigureAwait(false);
            return count;
        }

 
        private async void synced_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("syncronization ...", MaskType.Black);
            btnSync.IsEnabled = false;
            try
            {
                await HandleClickAsync();

            }
            finally
            {
                btnSync.IsEnabled = true;
                UserDialogs.Instance.HideLoading();
            }
            loading();
        }
        private async Task HandleClickAsync()
        {

            ContextModelDefault a = new ContextModelDefault("1");// Application.Current.Properties["ID"].ToString());
            CacheRefreshStatistics results = await a.Sync();
            if (results.Error != null)
            {
                await DisplayAlert("Sync result", "An error occured during sync session : " + Environment.NewLine + results.Error.Message, "Ok");

            }
            else
            {
                String message = "Sync session completed: " + Environment.NewLine +
                           "Total downloads :" + results.TotalDownloads + Environment.NewLine +
                           "Total uploads   :" + results.TotalUploads + Environment.NewLine +
                           "Total conflicts :" + results.TotalSyncErrors + Environment.NewLine +
                           "Ellapsed Time   :" + results.EndTime.Subtract(results.StartTime).TotalSeconds + " s.";

                await DisplayAlert("Sync result", message, "Ok");

            }
        }


        class SyncTableViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<SyncedTables> SyncedTablelist { get; set; }

            public SyncTableViewModel()
            {
                SyncedTablelist = new ObservableCollection<SyncedTables>(new[]
                {
                new SyncedTables() { Title = "States", TableName = "States", TableRows = App.database.TotalTableRows("States"), SyncStatus = false },
                new SyncedTables() { Title = "Items", TableName = "Items", TableRows = App.database.TotalTableRows("Items"), SyncStatus = false },
                new SyncedTables() { Title = "Countries", TableName = "Countries", TableRows = App.database.TotalTableRows("Countries"), SyncStatus = false },
                new SyncedTables() { Title = "Reasons", TableName = "Reasons", TableRows = App.database.TotalTableRows("Reasons"), SyncStatus = false },
                new SyncedTables() { Title = "Districts", TableName = "Districts", TableRows = App.database.TotalTableRows("Districts"), SyncStatus = false },
                new SyncedTables() { Title = "Cities", TableName = "Cities", TableRows = App.database.TotalTableRows("Cities"), SyncStatus = false },
                new SyncedTables() { Title = "ItemPrices", TableName = "ItemPrices", TableRows = App.database.TotalTableRows("ItemPrices"), SyncStatus = false },
                new SyncedTables() { Title = "Warehouses", TableName = "Warehouses", TableRows = App.database.TotalTableRows("Warehouses"), SyncStatus = false },
                new SyncedTables() { Title = "Sub Categories", TableName = "ITMSubCat", TableRows = App.database.TotalTableRows("ITMSubCat"), SyncStatus = false },
                new SyncedTables() { Title = "Categories", TableName = "ITMCategory", TableRows = App.database.TotalTableRows("ITMCategory"), SyncStatus = false },
                new SyncedTables() { Title = "PriceList", TableName = "PriceList", TableRows = App.database.TotalTableRows("PriceList"), SyncStatus = false },
                new SyncedTables() { Title = "Inventory", TableName = "ItemWhs", TableRows = App.database.TotalTableRows("ItemWhs"), SyncStatus = false },
                new SyncedTables() { Title = "Customers", TableName = "Retailers", TableRows = App.database.TotalTableRows("Retailers"), SyncStatus = false },
                new SyncedTables() { Title = "Orders", TableName = "Orders", TableRows = App.database.TotalTableRows("Orders"), SyncStatus = false },
                new SyncedTables() { Title = "Notes", TableName = "Activities", TableRows = App.database.TotalTableRows("Activities"), SyncStatus = false },
                new SyncedTables() { Title = "Outlets", TableName = "Outlets", TableRows = App.database.TotalTableRows("Outlets"), SyncStatus = false },
                new SyncedTables() { Title = "Delivery", TableName = "Delivery", TableRows = App.database.TotalTableRows("Delivery"), SyncStatus = false },
                new SyncedTables() { Title = "Quotations", TableName = "Quotations", TableRows = App.database.TotalTableRows("Quotations"), SyncStatus = false },
                new SyncedTables() { Title = "Invoices", TableName = "Invoices", TableRows = App.database.TotalTableRows("Invoices"), SyncStatus = false },
                new SyncedTables() { Title = "Customer Class", TableName = "BPClass", TableRows = App.database.TotalTableRows("BPClass"), SyncStatus = false },
                new SyncedTables() { Title = "Customer Group", TableName = "BPGroup", TableRows = App.database.TotalTableRows("BPGroup"), SyncStatus = false },

            });

            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "SyncStatus")
            {
                if (PropertyChanged == null)
                    return;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}
