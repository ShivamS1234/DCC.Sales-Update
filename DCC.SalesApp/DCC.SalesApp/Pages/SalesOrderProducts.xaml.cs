using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesOrderProducts : TabbedPage
    {
        ObservableCollection<Models.SaleProduct> source = new ObservableCollection<Models.SaleProduct>();
        Default.Orders orders = null;
        Default.OrderItems orderItems = new Default.OrderItems();
        ObservableCollection<Default.ITMCategory> catagories = new ObservableCollection<Default.ITMCategory>();
        ObservableCollection<Default.ITMSubCat> subcatagories = new ObservableCollection<Default.ITMSubCat>();
        public static Default.ITMCategory itmCat { get; set; }
        public static Default.ITMSubCat itmSubCat { get; set; }
        Int32 OrderID = -1;
        public SalesOrderProducts(Default.Orders _Orders)
        {
            InitializeComponent();
            orders = _Orders;
            OrderID = _Orders.ID;
            //pkrCatagory.HeightRequest = 30;
            //pkrSubCatagory.HeightRequest = 30;

            BarTextColor = Xamarin.Forms.Color.FromHex("#036890");
            source = App.database.getSaleProducts(OrderID);
            IteamDetail.ItemsSource = source;
            InitializePickers();            
            //IteamDetail.AutoFilterPanelHeight = 30;
            Theme.ApplyGridTheme();
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

            txtNTotal.Text = String.Format("{0:#,##0.00;(#,##0.00)}",(gtotal+ttotal) );
        }
        public void InitializePickers()
        {
            // pkrCatagory.ItemsSource = App.database.GetAllProductCatagory();
            pkrCatagory.Text = "Select Catagory";
           // pkrCatagory.Items.Insert(-1, "Hello");

        }
        private void pkrCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Default.ITMCategory cat = itmCat; //(Default.ITMCategory)pkrCatagory.SelectedItem;
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
            Default.ITMCategory cat = itmCat; //(Default.ITMCategory)pkrCatagory.SelectedItem;
            Default.ITMSubCat subCat = itmSubCat; //(Default.ITMSubCat)pkrSubCatagory.SelectedItem;
            if (subCat == null)
            {
                IteamDetail.ItemsSource = App.database.getQuotationProductsByCatagory(cat.ID, 0);
                return;
            }
            IteamDetail.ItemsSource = App.database.getQuotationProductsByCatagory(cat.ID, subCat.ID);
        }


        private async void cSummary_Appearing(object sender, EventArgs e)
        {
            gridSummary.ItemsSource = null;

            List<Models.SaleProduct> oSaleList =  source.Where(x => x.SaleQty > 0).ToList();
            await Task.Delay(0);
            try
            {
                gridSummary.ItemsSource = oSaleList;
                setSummary();
            }
            catch (Exception ex)
            { }
        }

        //List<Models.SaleProduct> selectedProductList = new List<Models.SaleProduct>();

        //private void IteamDetail_EndRowEdit(object sender, DevExpress.Mobile.DataGrid.RowEditingEventArgs e)
        //{
        //    var obj = (DevExpress.Mobile.DataGrid.GridControl)sender;
        //    Models.SaleProduct sp = (Models.SaleProduct)obj.SelectedDataObject;
        //    if ((int)sp.SaleQty > 0)
        //    {
        //        selectedProductList.Add(sp);
        //        gridSummary.ItemsSource = null;
        //        gridSummary.ItemsSource = selectedProductList;
        //    }
        //}
         
        private void toolbarSave_Clicked(object sender, EventArgs e)
        {
            var source1 = (List<Models.SaleProduct>)gridSummary.ItemsSource;
            if (source1.Count == 0)
            { 
                DisplayAlert("Message", "No Product selected for Sales Order.", "OK");
                return;
            }
            string _newOrder = System.Guid.NewGuid().ToString();
            string _message = "Orders Updated.";
            if (OrderID <= 0)
            {
                orders.ID = App.database.nextTableID("Orders");
                orders.CGuid = _newOrder;
                _message = "Orders saved to database.";
            }

            
            orders.RoundingAmnt = Convert.ToDecimal(txtGTotal.Text);
            orders.TaxTotal = Convert.ToDecimal(txtTTotal.Text);

            App.database.insertOrder(orders);
            try
            {
                short i = 1;
                foreach (Models.SaleProduct product in source.Where(x => x.SaleQty > 0))
                {
                    if (product.LineId <= 0)
                    {
                        orderItems.ID = App.database.nextTableID("OrderItems");
                        orderItems.LineNum = i;
                        orderItems.ItemID = product.id;
                        orderItems.VATCode = product.SaleVat;
                        orderItems.ItemName = product.name;

                        if (OrderID > 0)
                            orderItems.CGuid = orders.CGuid;
                        else
                            orderItems.CGuid = _newOrder;
                    }
                    else
                    {
                        orderItems.ID = product.OrderItemId;
                        orderItems.LineNum = i;
                        orderItems.ItemID = product.id;
                        orderItems.VATCode = product.SaleVat;
                        orderItems.ItemName = product.name;
                        orderItems.CGuid = orders.CGuid;
                    }

                    orderItems.LineTax = Convert.ToDecimal(product.TaxPer * product.SaleQty);

                    orderItems.LineStatus = "x";
                    orderItems.Quantity = Convert.ToDecimal(product.SaleQty);
                    orderItems.DeliveredQty = 0;
                    orderItems.Rate = (decimal)product.price;
                    orderItems.OpenQty = Convert.ToDecimal(product.SaleQty);
                    App.database.insertOrderItems(orderItems, product.LineId);
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
            //    Models.Customer customers = (Models.Customer)Application.Current.Properties["CustomerDetails"];
            //    if (customers.ID != 0)
            //    {
            //        orders.ID = App.database.nextTableID("Orders");
            //        orders.CustCode = customers.Code;
            //        orders.CustName = customers.Name;
            //        orders.CustRefNo = "280118";
            //        orders.ContPerson = customers.ID;
            //        orders.PostDate = DateTime.Now;
            //        orders.DueDate = DateTime.Now;
            //        orders.TaxDate = DateTime.Now;
            //        orders.RoundingAmnt = (decimal)gridSummary.TotalSummaries[0].SummaryValue;
            //        orders.UserId = 1;
            //        orders.CGuid = _newOrder;
            //        App.database.insertOrder(orders);

            //    }            
            //        try
            //        {
            //            short i = 1;
            //            foreach (Models.SaleProduct product in selectedProductList)
            //            {
            //                orderItems.ID = App.database.nextTableID("OrderItems");
            //                orderItems.CGuid = _newOrder;
            //                orderItems.LineNum = i;
            //                orderItems.ItemID = product.id;
            //                orderItems.ItemName = product.name;
            //                orderItems.LineStatus = "x";
            //                orderItems.Quantity = Convert.ToDecimal(product.SaleQty);
            //                orderItems.DeliveredQty = 0;
            //                orderItems.OpenQty = Convert.ToDecimal(product.SaleQty);
            //                App.database.insertOrderItems(orderItems);
            //                i++;                       
            //            }
            //            DisplayAlert("Message", "Order saved to database.", "OK");
            //        }
            //        catch (Exception ex)
            //        {
            //            System.Diagnostics.Debug.WriteLine(ex.Message);
            //            DisplayAlert("Message", "Error Occured. Please try again.", "OK");
            //        }




        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductCatagoryList() { Title = "Catagories" });
        }

        private void cProducts_Appearing(object sender, EventArgs e)
        {
            pkrCatagory.Text = itmCat == null ? "Select Catagory" : itmCat.Name;
            pkrSubCatagory.Text = itmSubCat == null ? "Select Sub-Catagory" : itmSubCat.Name;

            if (itmCat != null)
            {
                pkrCatagory_SelectedIndexChanged(null,null);                          
            }
            if (itmSubCat != null)
            {
                pkrSubCatagory_SelectedIndexChanged(null, null);
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductSubCatagoryList() { Title = " Sub Catagories" });
        }
    }
}