namespace DCC.SalesApp.Models
{

    public partial class UserSetting
    {

        private int _ID;

        private string _WebserverURL;

        private decimal _KMforNearBySearch;

        private string _DemoSystem;

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
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
        public string WebserverURL
        {
            get
            {
                return _WebserverURL;
            }
            set
            {
                _WebserverURL = value;

            }
        }

        public decimal KMforNearBySearch
        {
            get
            {
                return _KMforNearBySearch;
            }
            set
            { 
                _KMforNearBySearch = value; 
            }
        }

        public string DemoSystem
        {
            get
            {
                return _DemoSystem;
            }
            set
            { 
                _DemoSystem = value; 
            }
        }
    }
}
