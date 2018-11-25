
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Default;
using DCC.SalesApp.Models;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class InvoiceList : ContentPage
    {
        public InvoiceList()
        {
            InitializeComponent();
            //   OrderFullfill.ItemSelected += OrderFullfill_ItemSelected;
            BindOrder();
           // BindIteam();
        }
        public void BindOrder()
        {
            try
            {
                List<Invoices> objInvoice = App.Database.GetTransactionInvoice().ToList();
               // BindingContext = new InvoiceDetails { Invoices = objInvoice };
              listView.ItemsSource = objInvoice;


            }
            catch (Exception)
            {
                Debug.WriteLine("Error Occured...");
            }
        }

        public void BindIteam()
        {
            try
            {
                List<Models.InvoiceDetails> objInvoice = App.Database.GetInvoiceDetail().ToList();
                listView.ItemsSource = objInvoice;

            }
            catch (Exception)
            {
                Debug.WriteLine("Error Occured...");
            }

        }



    }
}