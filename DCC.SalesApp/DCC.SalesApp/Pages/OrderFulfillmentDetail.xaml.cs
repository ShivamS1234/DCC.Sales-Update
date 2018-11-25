using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using DCC.SalesApp.Models;
using System.Collections.Generic;
using System.IO;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderFulfillmentDetail : ContentPage
    {

    
        public OrderFulfillmentDetail(int _ID)
        {
            InitializeComponent();
            GetOrderDetail(_ID);


        //    GEtUsesrdetail();
               
        }
        public void GEtUsesrdetail()
        { 
        List<Default.Users> lst = new List<Default.Users>();
        lst = App.database.GetAllUserDetails().ToList();
        OrderFullfillInfor.ItemsSource = lst;

        }
        public void GetOrderDetail(int _ID)
        {
           
            List<OrdersFullFill> ord = (List<OrdersFullFill>)App.Database.GetfullfillOrderOnly(_ID);
            OrderFullfillInfor.ItemsSource = ord;


        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] ItemImage = (byte[])value;
                var stream = new MemoryStream(ItemImage);
                retSource = ImageSource.FromStream(() => stream);
            }
            return retSource;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }




    }
}