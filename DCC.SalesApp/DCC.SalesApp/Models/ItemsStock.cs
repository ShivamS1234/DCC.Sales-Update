using Xamarin.Forms;
using System.IO;

namespace DCC.SalesApp.Models
{
    public class ItemsStock
    {
        private int _ID;

        private string _Code;

        private string _Name;

        private string _UOM;

        private System.Nullable<decimal> _OnHand;

        private System.Nullable<decimal> _Commited;

        private System.Nullable<decimal> _OnOrder;

        private System.Nullable<decimal> _SalesPrice;

        private System.Nullable<decimal> _WholePrice;

        private byte[] _ItemImage;

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
        public System.Nullable<decimal> Commited
        {
            get
            {
                return _Commited;
            }
            set
            {
                _Commited = value;
            }
        }
        public System.Nullable<decimal> Ordered
        {
            get
            {
                return _OnOrder;
            }
            set
            {
                _OnOrder = value;
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
        public System.Nullable<decimal> WholePrice
        {
            get
            {
                return _WholePrice;
            }
            set
            {
                _WholePrice = value;
            }
        }

        //public byte[] ItemImage
        //{
        //    get
        //    {
        //        return _ItemImage;
        //    }
        //    set
        //    {
        //        // base.OnPropertyChanging("ProfileImg");
        //        _ItemImage = value;
        //        // base.OnPropertyChanged("ProfileImg");
        //    }
        //}


        public Xamarin.Forms.ImageSource ItemImage 
        {
            get
            {
                return
                ImageSource.FromStream(() => new MemoryStream(_ItemImage));
            }
        }

    }
}
