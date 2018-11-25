using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DCC.SalesApp.Models;
using System.Collections.Generic;
using Default;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoiceDetail : ContentPage
    {
         
        public InvoiceDetail(int _ID)
        {
            InitializeComponent();
            GetInvoiceDetail(_ID); 
               
        }
       
        public void GetInvoiceDetail(int _ID)
        {
            try
            {
                List<OrdersFullFill> oInvoiceItems = (List<OrdersFullFill>)App.Database.GetInvoicebyID(_ID);
                OrderFullfillInfor.ItemsSource = oInvoiceItems;
                Invoices _Invoice = App.Database.GetInvoice(_ID);
                InvoiceNo.Text = _Invoice.ID.ToString();
                InvoiceDate.Text = String.Format("{0:dd-MMM-yy}", _Invoice.PostDate);
                DueDate.Text = String.Format("{0:dd-MMM-yy}", _Invoice.DueDate);

                Tax.Text = String.Format("{0:#,##0.00;(#,##0.00);Zero}", _Invoice.TaxTotal);
                Total.Text = String.Format("{0:#,##0.00;(#,##0.00);Zero}", _Invoice.DocTotal);
                SubTotal.Text = String.Format("{0:#,##0.00;(#,##0.00);Zero}", (_Invoice.DocTotal - _Invoice.TaxTotal));
            }catch
            {

            }
        }
         
    }
}