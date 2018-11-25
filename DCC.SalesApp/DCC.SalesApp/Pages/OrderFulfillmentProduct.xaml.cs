using DevExpress.Mobile.DataGrid.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DCC.SalesApp.Helpers;
using System.IO;
using System.Collections.ObjectModel;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderFulfillmentProduct : ContentPage
    {

        Default.Delivery delivery = new Default.Delivery(); 
        Default.Orders objorder = new Default.Orders();
        ObservableCollection <Models.SaleProduct> source = new ObservableCollection<Models.SaleProduct>();
        Default.Quotations objQue = new Default.Quotations();
        public OrderFulfillmentProduct(Models.OrderFulfillNew obj)
        {
            InitializeComponent();
            ThemeManager.ThemeName = Themes.Light;

            //BarTextColor = Xamarin.Forms.Color.FromHex("#036890");
            InitializePickers();
            //pkrCatagory.HeightRequest = 30;
            //pkrSubCatagory.HeightRequest = 30;
            bindGrid(obj.CGuid);
        }

        void bindGrid(string CGuid)
        {
            source = App.database.getOrderFulfillmentProduct(CGuid);
            IteamDetail.ItemsSource = source;
        }
        public void InitializePickers()
        {
            
            pkrCatagory.ItemsSource = App.database.GetAllProductCatagory();            

        }
        private void toolbarSave_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Models.SaleProduct> lst = ((ObservableCollection<Models.SaleProduct>)IteamDetail.ItemsSource);
            App.database.updateOrderItems(lst.ToList()); System.Diagnostics.Debug.WriteLine("OrderItems Updated.");            
            update_Delivery();
            //Navigation.PopToRootAsync();
        }
        private  void toolbarSignature_Clicked(object sender, EventArgs e)
        {
            try
            {
                this.Navigation.PushAsync(new AddSignature(delivery, null));
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }


        private void update_Delivery()
        {
            try
            {
                string guid = source[0].CGuid;
                List<Default.Orders> orders = App.database.GetOrders(guid).ToList();
                List<Default.OrderItems> orderItems = App.database.GetOrdersItems(guid).ToList();

                            
                orders[0].CopyProperties(delivery);
                delivery.PostDate = System.DateTime.Now;
                delivery.TaxDate = System.DateTime.Now;
                delivery.CreatedDate = System.DateTime.Now;
                delivery.DueDate = System.DateTime.Now;
                delivery.UserId = Convert.ToInt32(Application.Current.Properties["ID"].ToString());
                delivery.ID = App.database.nextTableID("Delivery");
                int i = App.database.insertDelivery(delivery);

                //List<Default.DeliveryItems> deliveryItems = new List<Default.DeliveryItems>();

                bindGrid(guid);
                foreach (Default.OrderItems orderItem in orderItems)
                {                   
                    Default.DeliveryItems deliveryItem = new Default.DeliveryItems();
                    orderItem.CopyProperties(deliveryItem);
                    
                    ObservableCollection<Models.SaleProduct> sp = (ObservableCollection<Models.SaleProduct>)IteamDetail.ItemsSource;
                    var qty = sp.ToList().Find(x => x.CGuid == orderItem.CGuid && x.id == orderItem.ItemID);

                    deliveryItem.DeliveredQty = deliveryItem.Quantity-(decimal)qty.SaleQty;            

                    deliveryItem.ID = App.database.nextTableID("DeliveryItems");
                    App.database.insertDeliveryItems(deliveryItem);
                    //deliveryItems.Add(deliveryItem);
                }

                DisplayAlert("Message", "Order delivery saved.", "OK");
                Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                DisplayAlert("Alert", "Error Occured.", "OK");
            }

        }
        public byte[] UpdateImageinByte(dynamic file)
        {
            try
            {
                using (var memory = new MemoryStream())
                {
                    file.GetStream().CopyTo(memory);
                    file.Dispose();
                    return memory.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        List<Models.SaleProduct> selectedProductList = new List<Models.SaleProduct>();
        private void IteamDetail_EndRowEdit(object sender, DevExpress.Mobile.DataGrid.RowEditingEventArgs e)
        {

        }

        private void pkrCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Default.ITMCategory cat = (Default.ITMCategory)pkrCatagory.SelectedItem;
                pkrSubCatagory.ItemsSource = App.database.GetAllProductSubCatagory(cat.ID);
                //IteamDetail.ItemsSource = App.database.getQuotationProductsByCatagory(cat.ID);
                if (pkrCatagory.HeightRequest == 30)
                {
                    pkrCatagory.HeightRequest = 31;
                    pkrSubCatagory.HeightRequest = 31;
                }
                else
                {
                    pkrCatagory.HeightRequest = 30;
                    pkrSubCatagory.HeightRequest = 30;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void pkrSubCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}