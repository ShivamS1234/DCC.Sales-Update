using Default;
using System.Collections.Generic;

namespace DCC.SalesApp.Models
{
    public class InvoiceDetails
    {
        public List<Invoices> Invoices  { get; set; }
        private int _ID;

        private string _ItemCode ;

        private string _ItemName ;

        private string _UOM;


        private System.Nullable<decimal> _Qty;

        private System.Nullable<decimal> _Price;

        private System.Nullable<decimal> _Vat;

        private System.Nullable<decimal> _Total ;

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
        public string ItemCode 
        {
            get
            {
                return _ItemCode;
            }
            set
            {

                _ItemCode = value;

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

        public string UOM
        {
            get
            {
                return _UOM ;
            }
            set
            {
                _UOM = value;
            }
        }


        public System.Nullable<decimal> Qty 
        {
            get
            {
                return _Qty;
            }
            set
            {
                _Qty = value;
            }
        }
        public System.Nullable<decimal> Price 
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
            }
        }
        public System.Nullable<decimal> Vat 
        {
            get
            {
                return _Vat;
            }
            set
            {
                _Vat = value;
            }
        }
        public System.Nullable<decimal> Total 
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
            }
        }


    }
}
