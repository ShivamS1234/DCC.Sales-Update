
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerList : ContentPage
    {
        public CustomerList()
        {
            InitializeComponent();
            listView.ItemsSource = App.database.GetAllCustomer();
        }

        private bool _canClose = true;
        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            if (_canClose)
            {
                ShowExitDialog();
            }
            return _canClose;
        }
        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Want to go Dashboard Screen?", "Yes", "No");
            if (answer)
            {
                App.Current.MainPage = new MainPage();
                _canClose = false;
                //OnBackButtonPressed;
            }
        }  
    }
}