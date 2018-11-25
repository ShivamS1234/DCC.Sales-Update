using System.IO;
using Xamarin.Forms;

namespace DCC.SalesApp.Models
{
    public class AddressInfo : BaseViewModel
    {
        private int _ID;
        private string _CustomerName;
        private string _Address;
        private string _PinCode;
        private string _MobileNo;
        private string _Email;

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

        public string CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                // base.OnPropertyChanging("Code");
                _CustomerName = value;
                //  base.OnPropertyChanged("Code");
            }
        }

        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _Address = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string PinCode
        {
            get
            {
                return _PinCode;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _PinCode = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string MobileNo
        {
            get
            {
                return _MobileNo;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _MobileNo = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string EmailID
        {
            get
            {
                return _Email;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _Email = value;
                //  base.OnPropertyChanged("Name");
            }
        }
    }
}

