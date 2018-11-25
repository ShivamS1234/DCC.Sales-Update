using Acr.UserDialogs;
using DCC.SalesApp.Pages.Master;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DCC.SalesApp.ViewModels.RetailerVM
{
    public class AddRetailerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Default.Areas> AreasRepository;
        private ObservableCollection<Default.BPGroup> BPGroupRepository;
        private ObservableCollection<Default.BPClass> BPClassRepository;
        private Default.BPClass _selectedBPClass;
        private Default.BPGroup _selectedBPGroup;
        private Default.Areas _selectedArea;
        public static Default.Retailers _newRetailer;

        private int _ID;
        private string _Code;
        private string _Name;
        private string _Address;
        private System.Nullable<int> _AreaID;
        private string _Owner;
        private string _OwnerMobileNo;
        private string _OwnerEmail;
        private System.Nullable<int> _BPGroup;
        private System.Nullable<int> _BPClass;
        private System.Nullable<decimal> _Balance;
        private System.Nullable<decimal> _CrLimit;
        private INavigation navigation;

        public string Code
        {
            get
            {
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
                return _Address;
            }
            set
            {
               
                _Address = value;
                RaisePropertyChanged();
            }
        }
        public System.Nullable<int> AreaID
        {
            get
            {
                return _AreaID;
            }
            set
            {
                
                _AreaID = value;
                RaisePropertyChanged();
            }
        }
        public string Owner
        {
            get
            {
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
                return _OwnerEmail;
            }
            set
            {
                
                _OwnerEmail = value;
                RaisePropertyChanged();
            }
        }
        public System.Nullable<int> BPGroup
        {
            get
            {
                return _BPGroup;
            }
            set
            {
               
                _BPGroup = value;
                RaisePropertyChanged();
            }
        }
        public System.Nullable<int> BPClass
        {
            get
            {
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
                return _CrLimit;
            }
            set
            {
                
                _CrLimit = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Default.Areas> areas
        {
            get
            {
                return AreasRepository;
            }
            set
            {
                AreasRepository = value;
            }
        }
        public ObservableCollection<Default.BPGroup> bpGroup
        {
            get
            {
                return BPGroupRepository;
            }
            set
            {
                BPGroupRepository = value;

            }
        }
        public ObservableCollection<Default.BPClass> bpClass
        {
            get
            {
                return BPClassRepository;
            }
            set
            {
                BPClassRepository = value;
            }
        }
        public Default.BPClass selectedBPClass
        {
            get
            {
                return _selectedBPClass;
            }
            set
            {
                _selectedBPClass = value;
                RaisePropertyChanged();
            }
        }
        public Default.BPGroup selectedBPGroup
        {
            get
            {
                return _selectedBPGroup;
            }
            set
            {
                _selectedBPGroup = value;
                RaisePropertyChanged();
            }
        }
        public Default.Areas selectedArea
        {
            get
            {
                return _selectedArea;
            }
            set
            {
                _selectedArea = value;
                RaisePropertyChanged();
            }
        }
        public AddRetailerViewModel(INavigation _navigation)
        {
            AddNewCommand = new Command<object>(SaveRecords);
            areas = new ObservableCollection<Default.Areas>( App.database.GetAllLocation());
            bpGroup = App.database.GetBPGroups();
            bpClass = App.database.GetBPClasses();
            Code = App.database.nextTableID("Retailers").ToString();
            navigation = _navigation;

        }
        public Command<object> AddNewCommand { get; set; }

        private async void SaveRecords(object obj)
        {
            if (_selectedArea == null || selectedBPClass == null || selectedBPGroup == null )
            {
                await Application.Current.MainPage.DisplayAlert("Message", "could not blank Area, BP Group and BP Class!", "OK");
                return;
            }

            _newRetailer = new Default.Retailers();
            _newRetailer.ID= int.Parse(Code);
            _newRetailer.Code = _Code;
            _newRetailer.Name = _Name;
            _newRetailer.Address = _Address;
            _newRetailer.AreaID = _selectedArea.ID;
            _newRetailer.Owner = _Owner;
            _newRetailer.OwnerMobileNo = _OwnerMobileNo;
            _newRetailer.OwnerEmail = _OwnerEmail;
            _newRetailer.BPGroup = _selectedBPGroup.ID;
            _newRetailer.BPClass = _selectedBPClass.ID;
            _newRetailer.Balance = _Balance;
            _newRetailer.CrLimit = _CrLimit;
            _newRetailer.CreatedDate = DateTime.Now.Date;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync();
            _newRetailer.Latitude =Convert.ToDecimal(position.Latitude);
            _newRetailer.Longitude = Convert.ToDecimal(position.Longitude);            

            UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);            
            try
            {
                //var success = App.database.AddRetailer(_newRetailer);
                navigation.PushAsync(new AddressListPage() { Title = "Customer Address List" });
                //await Application.Current.MainPage.DisplayAlert("Message", "Customer inserted successfully.", "OK");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Alert!", "Somthing went wrong.\nPlease try again later.", "OK");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }



        }
        private void RaisePropertyChanged([CallerMemberName] string name=null)
        {           
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
