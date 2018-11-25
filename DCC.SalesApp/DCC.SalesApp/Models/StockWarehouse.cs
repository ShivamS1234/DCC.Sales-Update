namespace DCC.SalesApp.Models
{
    public  class StockWarehouse
    {

        private int _ID;
        private string _Code;
        private string _Name;
        private System.Nullable<System.DateTime> _CreatedDate;
        private string _Location ;
        private string _ZipCode;
        private string _City;

        [Microsoft.Synchronization.ClientServices.SQLite.PrimaryKey()]
        [Microsoft.Synchronization.ClientServices.SQLite.AutoIncrement()]
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
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
               // base.OnPropertyChanging("Code");
                _Code = value;
              //  base.OnPropertyChanged("Code");
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
              //  base.OnPropertyChanging("Name");
                _Name = value;
              //  base.OnPropertyChanged("Name");
            }
        }

        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
           //     base.OnPropertyChanging("CreatedDate");
                _CreatedDate = value;
               // base.OnPropertyChanged("CreatedDate");
            }
        }

       

        public string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                // base.OnPropertyChanging("Location");
                _Location = value;
                // base.OnPropertyChanged("Location");
            }
        }
        public string ZipCode
        {
            get
            {
                return _ZipCode;
            }
            set
            {
              //  base.OnPropertyChanging("ZipCode");
                _ZipCode = value;
             //   base.OnPropertyChanged("ZipCode");
            }
        }
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
              //  base.OnPropertyChanging("City");
                _City = value;
              //  base.OnPropertyChanged("City");
            }
        }

        public string NameCode
        {
            get { return string.Format("{0} ({1})", Name, Code); }
        }
        public string LocationCity
        {
            get { return string.Format("{0}, {1}", Location, City); }
        }




    }

}

