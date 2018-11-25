using DCC.SalesApp.Pages.RetailersView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DCC.SalesApp.ViewModels.RetailerVM
{
    [Preserve(AllMembers = true)]
    public class RetailerListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Default.Retailers> RetailersRepository;
        private Default.Retailers _selectedRetailer;        
        public ObservableCollection<Default.Retailers> retailers
        {
            get
            {
                return RetailersRepository;
            }
            set
            {
                RetailersRepository = value;
                RaisePropertyChanged();
            }
        }
        public Default.Retailers selectedRetailer
        {
            get
            {
                return _selectedRetailer;
            }
            set
            {
                _selectedRetailer = value;
                RaisePropertyChanged();
            }
        }
        public RetailerListViewModel()
        {
            AddNewCommand = new Command<object>(NavigateAddNewPage);
            RetailerDetailCommand = new Command<object>(NavigateDetailPage);            
            retailers = new ObservableCollection<Default.Retailers>( App.Database.GetAllCustomerDetails());
        }
        private void NavigateAddNewPage(object obj)
        {
            var sampleView = obj as ContentPage;
            var newRetailer = new AddRetailer();
            newRetailer.Title = "Add New Retailer";
            //ordersPage.BindingContext = this;
            sampleView.Navigation.PushAsync(newRetailer);
        }
        private void NavigateDetailPage(object obj)
        {            
            var sampleView = obj as Xamarin.Forms.Binding;
            var parentPage = sampleView.Source as ContentPage;            
            RetailerDetailsViewModel.cRetailer = selectedRetailer;
            var retailerDetails = new RetailerDetails();
            retailerDetails.Title = selectedRetailer.Name + " Details";
            //retailerDetails.BindingContext = selectedRetailer;
            parentPage.Navigation.PushAsync(retailerDetails);
            
        }       
        public Command<object> AddNewCommand { get; set; }

        public Command<object> RetailerDetailCommand { get; set; }
        
        private void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
