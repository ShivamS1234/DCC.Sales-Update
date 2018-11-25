using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using DCC.SalesApp.Models;
using DCC.SalesApp.Pages;
using DCC.SalesApp.ViewModels.RetailerVM;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace DCC.SalesApp.ViewModels.CommonVM
{
    public class NewAddressVM : BaseViewModel
    {
        public ICommand SubmittCommand { protected set; get; }
        INavigation navigation;
        Default.Address _newRetailer;

        public string contactPersonName;
        public string ContactPersonName
        {
            get { return contactPersonName; }
            set
            {
                if (value != null)
                {
                    contactPersonName = value;
                    this.OnPropertyChanged("ContactPersonName");
                }
            }
        }
        public string addresLine1;
        public string AddressLine1
        {
            get { return addresLine1; }
            set
            {
                if (value != null)
                {
                    addresLine1 = value;
                    this.OnPropertyChanged("AddressLine1");
                }
            }
        }
        public string addresLine2;
        public string AddressLine2
        {
            get { return addresLine2; }
            set
            {
                if (value != null)
                {
                    addresLine2 = value;
                    this.OnPropertyChanged("AddressLine2");
                }
            }
        }

        public string streetAddress;
        public string StreetAddress
        {
            get { return streetAddress; }
            set
            {
                if (value != null)
                {
                    streetAddress = value;
                    this.OnPropertyChanged("StreetAddress");
                }
            }
        }

        public string pinCode;
        public string PinCode
        {
            get { return pinCode; }
            set
            {
                if (value != null)
                {
                    pinCode = value;
                    this.OnPropertyChanged("PinCode");
                }
            }
        }

        public string city;
        public string City
        {
            get { return city; }
            set
            {
                if (value != null)
                {
                    city = value;
                    this.OnPropertyChanged("City");
                }
            }
        }

        public string state;
        public string State
        {
            get { return state; }
            set
            {
                if (value != null)
                {
                    state = value;
                    this.OnPropertyChanged("State");
                }
            }
        }

        public string contactNo;
        public string ContactNo
        {
            get { return contactNo; }
            set
            {
                if (value != null)
                {
                    contactNo = value;
                    this.OnPropertyChanged("ContactNo");
                }
            }
        }

        public string latitude;
        public string Latitude
        {
            get { return latitude; }
            set
            {
                if (value != null)
                {
                    latitude = value;
                    this.OnPropertyChanged("Latitude");
                }
            }
        }

        public string longitude;
        public string Longitude
        {
            get { return longitude; }
            set
            {
                if (value != null)
                {
                    longitude = value;
                    this.OnPropertyChanged("Longitude");
                }
            }
        }

        public NewAddressVM(INavigation _navigation)
        {
            navigation = _navigation;
            SubmittCommand = new Command(OnSubmit);
        }

        public async void OnSubmit()
        {
            try
            {
                _newRetailer = new Default.Address();
                _newRetailer.CustomerCode = AddRetailerViewModel._newRetailer.Code;
                _newRetailer.CustomerName = ContactPersonName;
                _newRetailer.Address1 = AddressLine1;
                _newRetailer.Address2 = AddressLine2;
                _newRetailer.StreetAddress = StreetAddress;
                _newRetailer.PinCode = int.Parse(PinCode);
                _newRetailer.City = City;
                _newRetailer.State = State;
                _newRetailer.MobileNo = ContactNo;
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync();
                _newRetailer.Latitude =Convert.ToDecimal(position.Latitude);
                _newRetailer.Longitude = Convert.ToDecimal(position.Longitude);            

                UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);            
                try
                {
                    var success = App.database.AddCustomerAddress(_newRetailer);
                    await Application.Current.MainPage.DisplayAlert("Message", "Customer Address inserted successfully.", "OK");
                    await navigation.PopAsync();
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region ViewModle_NewNotes

        #endregion
    }
}
