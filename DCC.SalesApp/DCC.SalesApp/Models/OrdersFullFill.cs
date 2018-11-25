using System.IO;
using Xamarin.Forms;

namespace DCC.SalesApp.Models
{
    public class OrdersFullFill
    {

        private int _ID;

        private string _CustCode;

        private string _OrderName;
        private string _Quantity;
        private string _UnitPrice;

        private string _CustRefNo;
        private string _OpenQty;

        private byte[] _ItemImage;


        private string _CustName;
        private string _ItemName;





        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;

            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(50)]
        public string CustCode
        {
            get
            {
                return _CustCode;
            }
            set
            {

                _CustCode = value;

            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string OrderName
        {
            get
            {
                return _OrderName;
            }
            set
            {
                _OrderName = value;
            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string UnitPrice
        {
            get
            {
                return _UnitPrice;
            }
            set
            {
                _UnitPrice = value;
            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string CustRefNo
        {
            get
            {
                return _CustRefNo;
            }
            set
            {
                _CustRefNo = value;
            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string OpenQty
        {
            get
            {
                return _OpenQty;
            }
            set
            {
                _OpenQty = value;
            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string CustName
        {
            get
            {
                return _CustName;
            }
            set
            {
                _CustName = value;
            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
            }
        }



        public byte[] ItemImage
        {
            get
            {
                return _ItemImage;
            }
            set
            {
                // base.OnPropertyChanging("ProfileImg");
                _ItemImage = value;
                // base.OnPropertyChanged("ProfileImg");
            }
        }
        //public MemoryStream ms
        //{
        //    get
        //    {
        //        MemoryStream stream = new MemoryStream(ItemImage);
        //        return stream;
        //    }

        //}

        public Xamarin.Forms.ImageSource stream
        {
            get
            {
                return
                ImageSource.FromStream(() => new MemoryStream(_ItemImage));
            }
        }


    }

}
