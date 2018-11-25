using Default;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transaction : ContentPage
    {
        Activities objActivities = new Activities();

        public Transaction()
        {
            InitializeComponent();
            Transactions_view.Transactions.AutoFilterPanelHeight = 30;
            Transactions_view.Transactions.RowTap += GrdTransaction_RowTap;
            BindTransactions();
        } 
        public void BindTransactions()
        {
            try
            {
                Theme.ApplyGridTheme();
                List<Models.Transaction> objetranject = App.Database.GetTransactions(1).ToList();
                Transactions_view.Transactions.ItemsSource = objetranject;
                ThemeManager.RefreshTheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
       

        private void GrdTransaction_RowTap(object sender, RowTapEventArgs e)
        {
            Int32 _selectedId = -1;
            try
            {

                Models.Transaction obj = new Models.Transaction();
                obj = Transactions_view.Transactions.SelectedDataObject as Models.Transaction;
                if (_selectedId != obj.SalesOrder)
                {
                    _selectedId = obj.SalesOrder;

                    if (obj.TransactionType == "Order")
                    {
                        //  this.Navigation.PushAsync(new ProductSelectionNew(_selectedId) { Title = obj.CustName });
                    }
                    else if (obj.TransactionType == "Notes")
                    {
                        this.Navigation.PushAsync(new NotesDetail(_selectedId) { Title = obj.CustName });
                    }
                    else if (obj.TransactionType == "Quotation")
                    {
                        this.Navigation.PushAsync(new NotesDetail(_selectedId) { Title = obj.CustName });
                    }

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}