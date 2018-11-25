using DevExpress.Mobile.DataGrid.Theme;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DCC.SalesApp.Helpers;
using System.Collections.ObjectModel;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderFulfillments : ContentPage
    {
        Default.Delivery delivery = new Default.Delivery();
        ObservableCollection<Models.OrderFulfillNew> orderList = new ObservableCollection<Models.OrderFulfillNew>();
        public OrderFulfillments()
        {
            InitializeComponent();
            OrderFullFillMent_view.OrderFullFillMent.AutoFilterPanelHeight = 30;
            OrderFullFillMent_view.OrderFullFillMent.RowTap += _grdOrderFulfill_RowTap;
            BindOrderFullFilMent();
            //ThemeManager.ThemeName = Themes.Light;
            //List<Models.OrderFulfillNew> orderList = App.Database.GetAllorderFullFillNew().ToList();
            //_grdOrderFulfill.ItemsSource = orderList;

            //List<Default.RetailerContacts> rc = App.database.GetAllRetailerContact().ToList();
        }

        protected void BindOrderFullFilMent()
        {
            try
            {
                Theme.ApplyGridTheme();
                orderList = App.Database.GetAllorderFullFillNew("");
                OrderFullFillMent_view.OrderFullFillMent.ItemsSource = orderList;
                ThemeManager.RefreshTheme();
                OrderFullFillMent_view.OrderFullFillMent.AutoFilterPanelHeight = 30;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        int i = 0;
        private void _grdOrderFulfill_RowTap(object sender, DevExpress.Mobile.DataGrid.RowTapEventArgs e)
        {
            if (e.FieldName == "Flag")
            {
                return;
            }
            //  Models.OrderFulfillNew order = (Models.OrderFulfillNew)_grdOrderFulfill.SelectedDataObject;
            Models.OrderFulfillNew order = (Models.OrderFulfillNew)OrderFullFillMent_view.OrderFullFillMent.SelectedDataObject;
            if (order != null && i == 0)
            {
                Navigation.PushAsync(new OrderFulfillmentProduct(order) { Title = "Order Details" });
                i++;
            }
            else
            {
                i = 0;
                return;
            }
        }

        private void toolbarFulfill_Clicked(object sender, EventArgs e)
        {
            DevExpress.Mobile.DataGrid.GridControl _Grid = OrderFullFillMent_view.OrderFullFillMent;
            ObservableCollection<Models.OrderFulfillNew> lst = (ObservableCollection<Models.OrderFulfillNew>)_Grid.ItemsSource;

            int count= lst.ToList().Where(x => x.Flag == true).Count();
            if (count == 0)
            {
                DisplayAlert("Message", "No order selected to fulfill.","OK");
                return;
            }               
            App.database.updateOrderItemsAll(lst.ToList());
            updateDelivery();
            BindOrderFullFilMent();           
        }


        //List<Models.SaleProduct> source = new List<Models.SaleProduct>();
        private void updateDelivery()
        {
            try
            {
                DevExpress.Mobile.DataGrid.GridControl _Grid = OrderFullFillMent_view.OrderFullFillMent;
                ObservableCollection<Models.OrderFulfillNew> orderFulfillNew = (ObservableCollection<Models.OrderFulfillNew>)_Grid.ItemsSource;
                foreach (Models.OrderFulfillNew ofn in orderFulfillNew)
                {
                    if (ofn.Flag == true)
                    {
                        string guid = ofn.CGuid;
                        List<Default.Orders> orders = App.database.GetOrders(guid).ToList();
                        List<Default.OrderItems> orderItems = App.database.GetOrdersItems(guid).ToList();


                        orders[0].CopyProperties(delivery);
                        delivery.PostDate = System.DateTime.Now;
                        delivery.TaxDate = System.DateTime.Now;
                        delivery.CreatedDate = System.DateTime.Now;
                        delivery.DueDate = System.DateTime.Now;
                        delivery.UserId = (int)Application.Current.Properties["ID"];
                        delivery.ID = App.database.nextTableID("Delivery");
                        int i = App.database.insertDelivery(delivery);
                        foreach (Default.OrderItems orderItem in orderItems)
                        {
                            Default.DeliveryItems deliveryItem = new Default.DeliveryItems();
                            orderItem.CopyProperties(deliveryItem);
                            deliveryItem.DeliveredQty = orderItem.Quantity;
                            deliveryItem.OpenQty = 0;
                            //List<Models.SaleProduct> sp = (List<Models.SaleProduct>)IteamDetail.ItemsSource;
                            //var qty = sp.Find(x => x.CGuid == orderItem.CGuid && x.id == orderItem.ItemID);
                            //deliveryItem.DeliveredQty = (decimal)qty.SaleQty;
                            deliveryItem.ID = App.database.nextTableID("DeliveryItems");
                            App.database.insertDeliveryItems(deliveryItem);
                            //deliveryItems.Add(deliveryItem);
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                DisplayAlert("Message", "Saved to database", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", "Error Occured.", "OK");
            }
        }
    }
}