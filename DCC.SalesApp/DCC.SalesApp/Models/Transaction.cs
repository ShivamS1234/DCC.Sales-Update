namespace DCC.SalesApp.Models
{
    public  class Transaction
    {
        private int _ID;       
        private int  _SalesOrder;
        private string _TransactionType;
        private string _CustName ; 
        private string _DocTotal ;
        private System.Nullable<System.DateTime> _DueDate;


        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                // base.OnPropertyChanging("ID");
                _ID = value;
                // base.OnPropertyChanged("ID");
            }
        }

        public int SalesOrder 
        {
            get
            {
                return _SalesOrder;
            }
            set
            {
                // base.OnPropertyChanging("ID");
                _SalesOrder = value;
                // base.OnPropertyChanged("ID");
            }
        }    
        public string TransactionType 
        {
            get
            {
                return _TransactionType;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _TransactionType = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string CustName 
        {
            get
            {
                return _CustName;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _CustName = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string DocTotal 
        {
            get
            {
                return _DocTotal;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _DocTotal = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public System.Nullable<System.DateTime> DueDate 
        {
            get
            {
                return _DueDate;
            }
            set
            {
                //     base.OnPropertyChanging("CreatedDate");
                _DueDate = value;
                // base.OnPropertyChanged("CreatedDate");
            }
        }
    }
}
