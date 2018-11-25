using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DCC.SalesApp.Models;
using DCC.SalesApp.Pages.RetailersView;
using Xamarin.Forms;

namespace DCC.SalesApp.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        public ObservableCollection<Customer> _retailerLists;
        public ObservableCollection<Customer> RetailerLists
        {
            get { return _retailerLists; }
            set
            {
                if (value != null)
                {
                    _retailerLists = value;
                    this.OnPropertyChanged("RetailerLists");
                }
            }
        }
        public Command<object> AddNewCommand { get; set; }
        public CustomerViewModel()
        {
            RetailerLists = new ObservableCollection<Customer>();
            AddNewCommand = new Command<object>(NavigateAddNewPage);
            GetCustomerList();
        }

        public void GetCustomerList()
        {
            List<Customer> Retailerslist = App.Database.GetAllCustomerDetails().ToList();
            if (Retailerslist != null)
            {
                foreach (var cutomerData in Retailerslist)
                {
                    RetailerLists.Add(cutomerData);
                }
            }
        }
        private void NavigateAddNewPage(object obj)
        {
            var sampleView = obj as ContentPage;
            var newRetailer = new AddRetailer();
            newRetailer.Title = "Add Customer";
            //ordersPage.BindingContext = this;
            sampleView.Navigation.PushAsync(newRetailer);
        }
    }
}