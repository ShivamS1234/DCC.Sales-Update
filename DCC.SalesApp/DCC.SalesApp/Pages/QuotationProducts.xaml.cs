using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuotationProducts : TabbedPage
    {
        ObservableCollection<Models.SaleProduct> source = new ObservableCollection<Models.SaleProduct>();
        Default.Quotations quotation = null;
        Default.QuotationItems quotationItems = new Default.QuotationItems();
        ObservableCollection<Default.ITMCategory> catagories = new ObservableCollection<Default.ITMCategory>();
        ObservableCollection<Default.ITMSubCat> subcatagories = new ObservableCollection<Default.ITMSubCat>();
        Int32 QuotationID = -1;
        public static Default.ITMCategory itmCat { get; set; }
        public static Default.ITMSubCat itmSubCat { get; set; }
        public QuotationProducts(Default.Quotations _Quotation)
        {
            InitializeComponent();
            quotation = _Quotation;
            QuotationID = _Quotation.ID;

            BarTextColor = Xamarin.Forms.Color.FromHex("#036890");
            source = App.database.getQuotationProducts(QuotationID);
            IteamDetail.ItemsSource = source;
            InitializePickers();
            //IteamDetail.AutoFilterPanelHeight = 30;
            Theme.ApplyGridTheme();            

            
            //SwipeButtonInfo sf = new SwipeButtonInfo() { Caption = "Delete", ButtonName = "btnDelete", BackgroundColor = Xamarin.Forms.Color.Red, TextColor = Xamarin.Forms.Color.White };
            //IteamDetail.LeftSwipeButtons.Add(sf);
            //IteamDetail.SwipeButtonClick += IteamDetail_SwipeButtonClick;
        }

        //private void IteamDetail_SwipeButtonClick(object sender, SwipeButtonEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    DisplayAlert("Message", "Clicked", "OK");
        //}

        private void IteamDetail_EndRowEdit(object sender, DevExpress.Mobile.DataGrid.RowEditingEventArgs e)
        {
           DevExpress.Mobile.DataGrid.EditingRowAction ex = e.Action;
        }

        public void setSummary()
        {
            double gtotal = 0, ttotal = 0;
            List<Models.SaleProduct> x = (List<Models.SaleProduct>)gridSummary.ItemsSource;
            foreach (Models.SaleProduct sp in x)
            {
                gtotal += sp.price * sp.SaleQty;
                ttotal += (sp.SaleQty * sp.price * sp.TaxPer / 100);
            }
            txtGTotal.Text = String.Format("{0:#,##0.00;(#,##0.00)}", gtotal);
            txtTTotal.Text = String.Format("{0:#,##0.00;(#,##0.00)}", ttotal);

            txtNTotal.Text = String.Format("{0:#,##0.00;(#,##0.00)}", (gtotal + ttotal));
        }
        public void InitializePickers()
        {
            //pkrCatagory.ItemsSource = App.database.GetAllProductCatagory();
            pkrCatagory.Text = "Select Catagory";

        }

        private void pkrCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Default.ITMCategory cat = itmCat;//(Default.ITMCategory)pkrCatagory.SelectedItem;
                //pkrSubCatagory.ItemsSource = App.database.GetAllProductSubCatagory(cat.ID);
                IteamDetail.ItemsSource = App.database.getQuotationProductsByCatagory(cat.ID);

                //if (pkrCatagory.HeightRequest == 30)
                //{
                //    pkrCatagory.HeightRequest = 31;
                //    pkrSubCatagory.HeightRequest = 31;
                //}
                //else
                //{
                //    pkrCatagory.HeightRequest = 30;
                //    pkrSubCatagory.HeightRequest = 30;
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
        private void pkrSubCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Default.ITMCategory cat = itmCat;//(Default.ITMCategory)pkrCatagory.SelectedItem;
            Default.ITMSubCat subCat = itmSubCat; //(Default.ITMSubCat)pkrSubCatagory.SelectedItem;
            if (subCat == null)
            {
                IteamDetail.ItemsSource = App.database.getQuotationProductsByCatagory(cat.ID, 0);
                return;
            }
            IteamDetail.ItemsSource = App.database.getQuotationProductsByCatagory(cat.ID, subCat.ID);
        }

        private void cSummary_Appearing(object sender, EventArgs e)
        {
            gridSummary.ItemsSource = null;
            List<Models.SaleProduct> oQuotationsList = source.Where(x => x.SaleQty > 0).ToList();
            try
            {
                gridSummary.ItemsSource = oQuotationsList;
                gridSummary.ColumnsAutoWidth = true;
                setSummary();
            }
            catch (Exception ex)
            { }

        }


        private void toolbarSave_Clicked(object sender, EventArgs e)
        {
            var source1 = (List<Models.SaleProduct>)gridSummary.ItemsSource;
            if (source1.Count == 0)
            {
                DisplayAlert("Message", "No Product selected for Quotation.", "OK");
                return;
            }

            string _newQuotation = System.Guid.NewGuid().ToString();
            string _message = "Quotation updated.";

            if (QuotationID <= 0)
            {
                quotation.ID = App.database.nextTableID("Quotations");
                quotation.CGuid = _newQuotation;
                _message = "Quotation saved to database.";
            }

            quotation.RoundingAmnt = Convert.ToDecimal(txtGTotal.Text);
            quotation.TaxTotal = Convert.ToDecimal(txtTTotal.Text);

            App.database.insertQuotations(quotation);

            try
            {
                short i = 1;
                foreach (Models.SaleProduct product in source.Where(x => x.SaleQty > 0))
                {
                    if (product.LineId <= 0)
                    {
                        quotationItems.ID = App.database.nextTableID("QuotationItems");
                        quotationItems.LineNum = i;
                        quotationItems.ItemID = product.id;
                        quotationItems.VATCode = product.SaleVat;
                        quotationItems.ItemName = product.name;

                        if (QuotationID > 0)
                            quotationItems.CGuid = quotation.CGuid;
                        else
                            quotationItems.CGuid = _newQuotation;
                    }
                    else
                    {
                        quotationItems.ID = product.QuotationItemId;
                        quotationItems.LineNum = i;
                        quotationItems.ItemID = product.id;
                        quotationItems.VATCode = product.SaleVat;
                        quotationItems.ItemName = product.name;
                        quotationItems.CGuid = quotation.CGuid;
                    }

                    quotationItems.LineTax = Convert.ToDecimal(product.TaxPer * product.SaleQty);
                    /*added*/    //quotationItems.ID = product.id;
                    quotationItems.LineStatus = "x";
                    quotationItems.Quantity = Convert.ToDecimal(product.SaleQty);
                    quotationItems.DeliveredQty = 0;
                    quotationItems.Rate = (decimal)product.price;
                    quotationItems.OpenQty = Convert.ToDecimal(product.SaleQty);
                    App.database.insertQuotationsProducts(quotationItems, product.LineId);
                    i++;
                }
                DisplayAlert("Message", _message, "OK");
                Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                DisplayAlert("Message", "Error Occured. Please try again.", "OK");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductCatagoryList() { Title = "Catagories" });
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductSubCatagoryList() { Title = " Sub Catagories" });
        }

        private void cProducts_Appearing(object sender, EventArgs e)
        {
            pkrCatagory.Text = itmCat == null ? "Select Catagory" : itmCat.Name;
            pkrSubCatagory.Text = itmSubCat == null ? "Select Sub-Catagory" : itmSubCat.Name;

            if (itmCat != null)
            {
                pkrCatagory_SelectedIndexChanged(null, null);
            }
            if (itmSubCat != null)
            {
                pkrSubCatagory_SelectedIndexChanged(null, null);
            }
        }
    }
}