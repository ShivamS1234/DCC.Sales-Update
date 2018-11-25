using System.IO;
using Xamarin.Forms;

namespace DCC.SalesApp.Models
{
    public   class ItemStocks
    {
        private int _ID;

        private string _Code;

        private string _Name;

        private string _UOM;

        private System.Nullable<decimal> _SalesPrice;

        private System.Nullable<decimal> _OnHand;

        private byte[] _ItemImage;

        private string _Currency;

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
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {

                _Code = value;

            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(50)]
        public string Currency
        {
            get
            {
                return _Currency;
            }
            set
            {
                _Currency = value;
          }
        }

        public string UOM
        {
            get
            {
                return _UOM;
            }
            set
            {
                _UOM = value;
            }
        }
        public System.Nullable<decimal> OnHand
        {
            get
            {
                return _OnHand;
            }
            set
            {
                _OnHand = value;
            }
        }

        public System.Nullable<decimal> SalesPrice
        {
            get
            {
                return _SalesPrice;
            }
            set
            {
                _SalesPrice = value;
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
        public Xamarin.Forms.ImageSource stream
        {
            get
            {
                return
                ImageSource.FromStream(() => new MemoryStream(_ItemImage));
            }
        }

        public string NameCode
        {
            get { return string.Format("{0} ({1})", Name, Code); }
        }

        public string PriceQty 
        {
            get { return string.Format("{0} ({1})", SalesPrice, OnHand); }
        }

        public string CurrencyPrice
        {
            get { return string.Format("{0} {1}", Currency, SalesPrice); } 
        }


        public string UOMQty
        {
            get { return string.Format("{0}: {1}", UOM, OnHand); }
        }
    }
}
