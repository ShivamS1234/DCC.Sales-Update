using System;  
using System.ComponentModel;  
using System.Windows.Input;  
using Xamarin.Forms;
using DCC.SalesApp;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DCC.SalesApp.Pages;

namespace DCC.SalesApp.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        public Action CommendService, DisplayInvalidEmail, DisplayInvalidPassword, DisplayWrongEmail, DisplayWrongPassword, DisplayWrongUserType;
        private string emailID;
        private string cons_emailID;
        private string password;
        private bool isEnable;
        private string cons_password;
        private int usertype;
        private int cons_usertype;

        public ICommand SubmitCommand { protected set; get; }
        public ICommand SettingCommand { protected set; get; }
        INavigation navigation;

        public LogInPageViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            SubmitCommand = new Command(OnSubmit);
            SettingCommand = new Command(OnSetting);

            setLogInData();
        }

        private void setLogInData()
        {
            Email = "admin@admin.com";
            Password = "user@123";
        }

        public string Email
        {
            get { return emailID; }
            set
            {
                emailID = value;
                this.OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.OnPropertyChanged("Password");
            }
        }

        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                this.OnPropertyChanged("IsEnable");
            }
        }

       

        public async void OnSubmit()
        {
            UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);
            IsEnable = false;
            try
            {
                // Can't use ConfigureAwait here.
                await HandleClickAsync();
            }
            finally
            {
                // We are back on the original context for this method.
                IsEnable = true;
                UserDialogs.Instance.HideLoading();
            }
        }

        public async void OnSetting()
        {
            UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);
            await navigation.PushModalAsync(new UserSettingPage());
            UserDialogs.Instance.HideLoading();
        }

        public bool OnOfflineValidation()
        {
            if (!cons_emailID.Equals(emailID))
            {
                DisplayWrongEmail();
                return false;
            }
            else if (!cons_password.Equals(password))
            {
                DisplayWrongPassword();
                return false;
            }
            else if (!cons_usertype.Equals(usertype))
            {
                DisplayWrongUserType();
                return false;
            }
            return true;
        }

        private async Task HandleClickAsync()
        {
            try
            {
                if ((Email != null || Email=="") || (password != null || password == ""))
                {
                    var oUser = await App.PCManager.RefreshUserAsync(Email).ConfigureAwait(true);
                    if (oUser != null)
                    {
                        if (Password == oUser.Password)
                        {
                            // App.Database.SaveUserDetails(oUser.Result);
                            try
                            {
                                Application.Current.Properties["ID"] = oUser.ID;
                                Application.Current.Properties["Code"] = oUser.Code;
                                Application.Current.Properties["FirstName"] = oUser.FirstName;
                                Application.Current.Properties["Email"] = oUser.Email;
                                App.Current.MainPage = new DCC.SalesApp.Pages.UserSafetyPageNew();
                                // App.Current.MainPage = new MainPage();
                            }
                            catch (Exception ex)
                            {
                                UserDialogs.Instance.Alert("Error !!", ex.Message, "OK");
                            }
                        }
                        else
                            UserDialogs.Instance.Alert("Invalid Crendetials!", "It's seems that you have entered an incorrect email or password. Please try again.", "OK");

                    }
                    else
                        UserDialogs.Instance.Alert("Invalid Crendetials!", "It's seems that you have entered an incorrect email or password. Please try again.", "OK");
                }
                else
                {
                    UserDialogs.Instance.Alert("Invalid Crendetials!", "It's seems that you have entered an incorrect email or password. Please try again.", "OK");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

    }
}
