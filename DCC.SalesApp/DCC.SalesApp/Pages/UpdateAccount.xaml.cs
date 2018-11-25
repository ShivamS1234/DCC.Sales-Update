using DCC.SalesApp.Helpers;
using Default;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateAccount : TabbedPage
    {
        Users oUser = new Users();
        //   int _userId = 1;
        private MediaFile _MediaFile;
        public UpdateAccount(Users _User)
        {
            InitializeComponent();
            BarTextColor = Xamarin.Forms.Color.FromHex("#036890");
            oUser = _User;
            GetUserDetail();
            CameraButton.Clicked += CameraButton_Clicked;
            GalaryPhoto.Clicked += GalaryPhoto_Clicked;
        }

        private async void location_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();
            DependencyService.Get<IMapLocation>().GetCurrentLoacation(position.Latitude.ToString(), position.Longitude.ToString());
        }
        public void BtnUpdateEdit_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            int i = -1;
            oUser.Address = textAddress.Text.ToString();
            oUser.Email = textEmail.Text.ToString();
            oUser.Password = textPassword.Text.ToString();
            oUser.MobileNo = textMobileNum.Text.ToString();
            oUser.FirstName = textFistName.Text.ToString();
            oUser.MiddleName = txtMiddleName.Text.ToString();
            oUser.LastName = textLastName.Text.ToString();

            if (Application.Current.Properties.ContainsKey("photo"))
            {
                oUser.ProfileImg = UpdateImageinByte(Application.Current.Properties["photo"]);
            }
            else
            {
                Att_Image.Source = ImageSource.FromStream(() => new MemoryStream(oUser.ProfileImg));
            }

            try
            {
                i = App.Database.UpdateUserDetail(oUser);
            }
            catch (Exception EX)
            {
                throw EX;
            }
            Application.Current.Properties.Clear();
            if (i < 0)
            {
                DisplayAlert("Update Account", "Update Fail .. Please try again ", "OK");
                return;
            }
            else
            {
                DisplayAlert("Update Account", "Account update Success . ", "OK");
                return;
            }

        }

        public byte[] UpdateImageinByte(dynamic file)
        {
            using (var memory = new MemoryStream())
            {
                file.GetStream().CopyTo(memory);
                file.Dispose();
                return memory.ToArray();
            }
        }
        public void GetUserDetail()
        {
            try
            {

                textAddress.Text = oUser.Address;
                textFistName.Text = oUser.FirstName;
                txtMiddleName.Text = oUser.MiddleName;
                textLastName.Text = oUser.LastName;
                textEmail.Text = oUser.Email;
                textMobileNum.Text = oUser.MobileNo;
                textPassword.Text = oUser.Password;
                //   Att_Image.Source = oUser.ProfileImg.ToString();
                Att_Image.Source = ImageSource.FromStream(() => new MemoryStream(oUser.ProfileImg));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

                if (photo != null)
                {
                    Att_Image.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    Application.Current.Properties["photo"] = photo;
                }
            }

            catch (Exception EX)
            {
                throw EX;
            }
        }

        private async void GalaryPhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("No Photo Hear", "Photo not available ", "OK");
                    return;
                }
                _MediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_MediaFile != null)
                {
                    Att_Image.Source = ImageSource.FromStream(() => { return _MediaFile.GetStream(); });
                    Application.Current.Properties["photo"] = _MediaFile;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }









    }
}
