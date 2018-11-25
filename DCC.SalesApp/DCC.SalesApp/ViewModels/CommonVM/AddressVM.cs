using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DCC.SalesApp.Models;
using DCC.SalesApp.Pages;
using DCC.SalesApp.Pages.Master;
using Default;
using Xamarin.Forms;

namespace DCC.SalesApp.ViewModels.CommonVM
{
    public class AddressVM : BaseViewModel
    {
        public ICommand AddNewAddressCommand { protected set; get; }
        private INavigation navigation;
        public ObservableCollection<AddressInfo> _AddressLists;

        public ObservableCollection<AddressInfo> AddressLists
        {
            get { return _AddressLists; }
            set
            {
                if (value != null)
                {
                    _AddressLists = value;
                    this.OnPropertyChanged("AddressLists");
                }
            }
        }


        public bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value != null)
                {
                    isVisible = value;
                    this.OnPropertyChanged("IsVisible");
                }
            }
        }

        public AddressVM(INavigation _navigation)
        {
            AddressLists = new ObservableCollection<AddressInfo>();
            navigation = _navigation;
            AddNewAddressCommand = new Command(OnAddNewAddress);
            GetNotesList();
            IsVisible = false;
        }


        public async void OnAddNewAddress()
        {
            try
            {
                navigation.PushAsync(new NewAddressPage() { Title = "Add Customer Address" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetNotesList()
        {
            List<AddressInfo> oNotesList = App.Database.GetCustomerAddress().ToList();
            if (oNotesList != null)
            {
                AddressLists.Clear();
                foreach (var cutomerData in oNotesList)
                {
                    AddressLists.Add(cutomerData);
                }
            }
        }
    }
}
