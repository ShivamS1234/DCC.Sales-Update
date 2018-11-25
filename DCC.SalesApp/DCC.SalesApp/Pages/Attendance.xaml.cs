using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DCC.SalesApp.Views;
using Rg.Plugins.Popup.Extensions;
using System.IO;
using Default;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Attendance : ContentPage
    {
        UserAttendance OUserAttendance { get; set; } 
        public Attendance()
        {
            OUserAttendance = new UserAttendance();
            InitializeComponent();
            LblUserName.Text = "Deepak Kumar";// Application.Current.Properties["FirstName"].ToString()+" "+ Application.Current.Properties["LastName"].ToString();
            LblUserId.Text = "User Id: "+ Application.Current.Properties["ID"].ToString();
            LblAddress.Text = "New Delhi";// Application.Current.Properties["ID"].ToString();
            LblEmail.Text = "deepak@admin.com";// Application.Current.Properties["ID"].ToString();
            LblPhone.Text = "3131313";// Application.Current.Properties["ID"].ToString();
            LblDesignation.Text = "Sales Exe";// Application.Current.Properties["ID"].ToString();
            var tapImage = new TapGestureRecognizer();
            tapImage.Tapped += Image_Tapped;
            Att_Image.GestureRecognizers.Add(tapImage);
            btn_Absent.Clicked += AbsentConfirmationPopup;
            btn_Present.Clicked += PresentConfirmationPopup;
        }

        private async void Image_Tapped(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                Directory = "Sample",
                Name = "test.jpg"
            });
            
            if (file == null)
                return;
            Att_Image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();                 
                return stream;
            });

             OUserAttendance.LoginImage = imageToByteArray(file);
         

        }

        public byte[] imageToByteArray(dynamic file)
        {
            using (var ms = new MemoryStream())
            {
                file.GetStream().CopyTo(ms);
                file.Dispose();
                return ms.ToArray();
            }
        }

        private async void AbsentConfirmationPopup(object sender, EventArgs e)
        {

            OUserAttendance.IsAbsent = 1;

            var page = new AbsentConfirmation(OUserAttendance);
            try
            {
                await Navigation.PushPopupAsync(page);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error !!", ex.Message, "OK");
            }
        }
        private async void PresentConfirmationPopup(object sender, EventArgs e)
        {
            OUserAttendance.IsAbsent = 0;
            var page = new AbsentConfirmation(OUserAttendance);
            try
            {
                await Navigation.PushPopupAsync(page);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error !!", ex.Message, "OK");
            }
        }
    }
}