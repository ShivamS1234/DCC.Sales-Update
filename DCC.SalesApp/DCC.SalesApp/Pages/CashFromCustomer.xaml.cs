using Default;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CashFromCustomer  : ContentPage
    {
        CashReceipts oCash  = new CashReceipts();
        public CashFromCustomer()
        {
            InitializeComponent();
            initilizePickers();
        }
        public void initilizePickers()
        {
            try
            {
                PkCustomer.TextColor = Color.FromHex("#036890");
                PkCustomerName.TextColor = Color.FromHex("#036890");
                PkACurrency.TextColor = Color.FromHex("#036890");
                PkPayMeans.TextColor = Color.FromHex("#036890");  

                IEnumerable<Retailers> oCustomerCode = App.database.GetAllCustomer();
                PkCustomer.ItemsSource = oCustomerCode.ToList();

                IEnumerable<Retailers> oCustomerName  = App.database.GetAllCustomer();
                PkCustomerName.ItemsSource = oCustomerName.ToList();

                IEnumerable<Currencies> Currency  = App.database.GetAllCurrencies();
                PkACurrency.ItemsSource = Currency.ToList();

                IEnumerable<PayMeans> payment  = App.database.GetAllPayment();
                PkPayMeans.ItemsSource = payment.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Occured : " + ex);
            }
        }
            
        private void PkCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PkCustomer.SelectedIndex == -1)
                return;
            var _ActiCustomer = (Retailers)PkCustomer.SelectedItem;
        }

        private void PkCustomerNamee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PkCustomerName.SelectedIndex == -1)
                return;
            var _ActiCustomer = (Retailers)PkCustomerName.SelectedItem;
        }

        private void PkACurrency_SelectedIndexChanged (object sender, EventArgs e)
        {
            if (PkACurrency.SelectedIndex == -1)
                return;
            var _ActiCurrency = (Currencies)PkACurrency.SelectedItem;
        }

        private void PkPayMeans_SelectedIndexChanged (object sender, EventArgs e)
        {
            if (PkPayMeans.SelectedIndex == -1)
                return;
            var _ActiPayMeans   = (PayMeans)PkPayMeans.SelectedItem;
        }
        
        public void BtnUpdateEdit_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                int i = -1;
            
                Retailers Pcustomer = (Retailers)PkCustomer.SelectedItem;
                Retailers PcustomerName = (Retailers)PkCustomerName.SelectedItem;
                Currencies Pcurrency = (Currencies)PkACurrency.SelectedItem;
                PayMeans Paymeantd   = (PayMeans)PkPayMeans.SelectedItem;
                oCash.Id = App.database.nextTableID("CashReceipts");
                oCash.CustCode = (Pcustomer.Code);
                oCash.CustName = (PcustomerName.Name);
                oCash.Currency = (Pcurrency.CurrName);
                oCash.Mode = (Paymeantd.Id);
                oCash.Amount =Convert.ToDecimal(textAmount.Text.Trim());
                oCash.RefNo = texChequeReference.Text.Trim();
                oCash.ReceiptRemarks = texReceipt.Text.Trim();
                oCash.ReceivedFrom = texReceivedFrom.Text.Trim();
                oCash.ReceivedBy = texReceivedBy.Text.Trim();
                oCash.CreatedDate = DateTime.Today;
                oCash.UserId = Convert.ToInt32(Application.Current.Properties["ID"].ToString());

                i = App.database.SaveCustomerCash(oCash);
                Application.Current.Properties.Clear();

                if (i > 0)
                {
                    DisplayAlert("Save Notes ", "Your recods is submitted ", "OK");
                    return;
                }
                else
                {
                    DisplayAlert("Save Notes", "Update Fail .. Please try again ", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Occured : " + ex);

            }

        }

        public void BtnSignature_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
           

            try
            {
             //   this.Navigation.PushAsync(new SignatureCustomer(oCash));
              this.Navigation.PushAsync(new AddSignature(null, oCash));
            }
            catch (Exception ex )
            {
                System.Diagnostics.Debug.WriteLine("Exception Occured : " + ex);
            }

        }





        public byte[] UpdateImageinByte(dynamic file)
        {
            using (var memory = new MemoryStream())
            {
                file.GetStream().CopyTo(memory);
                file.Dispose();
                return memory.ToArray();
            }
        }

    }
}
