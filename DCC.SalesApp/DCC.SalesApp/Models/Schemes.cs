using Xamarin.Forms;
using System.IO;

namespace DCC.SalesApp.Models
{
    public class Schemes
    {
        private int _ID;
        private string _Code;
        private string _Name;
        private string _SchemeName;
        private string _SchemeCode;
        private string _Quantity;
        private string _FreeQty;
        private System.Nullable<decimal> _Discount;
        private int _SchemeType;
        private int _ApplicableTo;
        private byte[] _ItemImage;

        private System.Nullable<System.DateTime> _FromDate;
        private System.Nullable<System.DateTime> _ToDate;

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

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string SchemeName
        {
            get
            {
                return _SchemeName;
            }
            set
            {
                _SchemeName = value;
            }
        }

        [Microsoft.Synchronization.ClientServices.SQLite.MaxLength(254)]
        public string SchemeCode
        {
            get
            {
                return _SchemeCode;
            }
            set
            {
                _SchemeCode = value;
            }
        }

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

        public string FreeQty
        {
            get
            {
                return _FreeQty;
            }
            set
            {
                _FreeQty = value;
            }
        }
        public System.Nullable<decimal> Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                _Discount = value;
            }
        }

        public int SchemeType
        {
            get
            {
                return _SchemeType;
            }
            set
            {
                _SchemeType = value;

            }
        }


        public int ApplicableTo
        {
            get
            {
                return _ApplicableTo;
            }
            set
            {
                _ApplicableTo = value;

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
      
        public System.Nullable<System.DateTime> FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {

                _FromDate = value;

            }
        }

        public System.Nullable<System.DateTime> ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {

                _ToDate = value;

            }
        }

    }
}
