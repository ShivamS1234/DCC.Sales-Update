using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace DCC.SalesApp.ViewModels.RetailerVM
{
    [Preserve(AllMembers = true)]
    public class RetailerDetailsViewModel:INotifyPropertyChanged
    {
        public static Default.Retailers cRetailer { get; set; }

        private string _Code;

        private string _Name;

        private string _Address;

        private Default.Areas _Area;

        private string _Owner;

        private string _OwnerMobileNo;

        private string _OwnerEmail;

        private Default.BPGroup _BPGroup;

        private Default.BPClass _BPClass;

        private System.Nullable<decimal> _Balance;

        private System.Nullable<decimal> _CrLimit;

        private System.Nullable<System.DateTime> _CreatedDate;
        public string Code
        {
            get
            {
                _Code = cRetailer.Code;
                return _Code;
            }
            set
            {
                 
                _Code = value;
                RaisePropertyChanged();
            }
        }

      
        public string Name
        {
            get
            {
                _Name = cRetailer.Name;
                return _Name;
            }
            set
            {
                 
                _Name = value;
                RaisePropertyChanged();
            }
        }

        
        public string Address
        {
            get
            {
                _Address = cRetailer.Address;
                return _Address;
            }
            set
            {
                 
                _Address = value;
                RaisePropertyChanged();
            }
        }

        public Default.Areas Area
        {
            get
            {
                _Area = (App.database.getAreas()).Single(x => x.ID == cRetailer.AreaID);               
                return _Area;
            }
            set
            {
                 
                _Area = value;
                RaisePropertyChanged();
            }
        }

       
        public string Owner
        {
            get
            {
                _Owner = cRetailer.Owner;
                return _Owner;
            }
            set
            {
                
                _Owner = value;
                RaisePropertyChanged();
            }
        }

       
        public string OwnerMobileNo
        {
            get
            {
                _OwnerMobileNo = cRetailer.OwnerMobileNo;
                return _OwnerMobileNo;
            }
            set
            {
                 
                _OwnerMobileNo = value;
                RaisePropertyChanged();
            }
        }

       
        public string OwnerEmail
        {
            get
            {
                _OwnerEmail = cRetailer.OwnerEmail;
                return _OwnerEmail;
            }
            set
            {
                 
                _OwnerEmail = value;
                RaisePropertyChanged();
            }
        }

        public Default.BPGroup BPGroup
        {
            get
            {               
                _BPGroup = (App.database.GetBPGroups()).Single(x => x.ID == cRetailer.BPGroup);
                return _BPGroup;
            }
            set
            {
                 
                _BPGroup = value;
                RaisePropertyChanged();
            }
        }

        public Default.BPClass BPClass
        {
            get
            {
                 
                _BPClass = (App.database.GetBPClasses()).Single(x => x.ID == cRetailer.BPClass);
                return _BPClass;
            }
            set
            {
                 
                _BPClass = value;
                RaisePropertyChanged();
            }
        }

        public System.Nullable<decimal> Balance
        {
            get
            {
                _Balance = cRetailer.Balance;
                return _Balance;
            }
            set
            {
                 
                _Balance = value;
                RaisePropertyChanged();
            }
        }

        public System.Nullable<decimal> CrLimit
        {
            get
            {
                _CrLimit = cRetailer.CrLimit;
                return _CrLimit;
            }
            set
            {
               
                _CrLimit = value;
                RaisePropertyChanged();
            }
        }

        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                _CreatedDate = cRetailer.CreatedDate;
                return _CreatedDate;
            }
            set
            {               
                _CreatedDate = value;
                RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RetailerDetailsViewModel()
        {

        }
    }
}
