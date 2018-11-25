using Acr.UserDialogs;
using DCC.SalesApp.Data;
using Default;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteNew : ContentPage
    {
        Activities oAct = new Activities();
        private MediaFile _MediaFile;
        public NoteNew()
        {
            InitializeComponent();
            CameraButton.Clicked += CameraButton_Clicked;
            GalaryPhoto.Clicked += GalaryPhoto_Clicked;
            initilizePickers();
        }
        public void initilizePickers()
        {
            try
            {
                List<ActivityPriority> priority = new List<ActivityPriority>();
                priority.Add(new ActivityPriority { ID = "H", Priorty = "High" });
                priority.Add(new ActivityPriority { ID = "M", Priorty = "Medium" });
                priority.Add(new ActivityPriority { ID = "L", Priorty = "Low" });


                List<ActivityLStatus> LStatus = new List<ActivityLStatus>();
                LStatus.Add(new ActivityLStatus { ID = 0, Status = "True" });
                LStatus.Add(new ActivityLStatus { ID = 1, Status = "False" });

                IEnumerable<ActivitySubjects> Subject = App.database.GetAllSubject();
                PkSubjectype.ItemsSource = Subject.ToList();

                PkSubjectype.TextColor = Color.FromHex("#036890");
                PkActiName.TextColor = Color.FromHex("#036890");
                PkActTypeID.TextColor = Color.FromHex("#036890");
                PkLocation.TextColor = Color.FromHex("#036890");
                PkPriority.TextColor = Color.FromHex("#036890");
                PkStatus.TextColor = Color.FromHex("#036890");
                PkCustomer.TextColor = Color.FromHex("#036890");

                IEnumerable<ActivityActions> Activity = App.database.GetAllActiviStatus();
                PkActiName.ItemsSource = Activity.ToList();

                IEnumerable<ActivityTypes> ActiType = App.database.GetAllActivityType();
                PkActTypeID.ItemsSource = ActiType.ToList();

                IEnumerable<Areas> oLocation = App.database.GetAllLocation();
                PkLocation.ItemsSource = oLocation.ToList();

                PkPriority.ItemsSource = priority.ToList();

                PkStatus.ItemsSource = LStatus.ToList();

                IEnumerable<Retailers> oCustomer = App.database.GetAllCustomer();
                PkCustomer.ItemsSource = oCustomer.ToList();


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Occured : " + ex);
            }
        }

        private void PkSubjectype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PkSubjectype.SelectedIndex == -1)
                return;
            var _cat_id = (ActivitySubjects)PkSubjectype.SelectedItem;
        }

        private void PkActiName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PkActiName.SelectedIndex == -1)
                return;
            var _Acti_id = PkActiName.SelectedItem;
        }

        private void PkActTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PkActTypeID.SelectedIndex == -1)
                return;
            var _ActiType = (ActivityTypes)PkActTypeID.SelectedItem;
        }

        private void PkPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PkPriority.SelectedIndex == -1)
                return;
            var _ActiPriority = (ActivityPriority)PkPriority.SelectedItem;
        }

        private void PickerPkStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (PkStatus.SelectedIndex == -1)
                return;

            var _ActiStatus = (ActivityLStatus)PkStatus.SelectedItem;
        }
        private void PkCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PkCustomer.SelectedIndex == -1)
                return;
            var _ActiCustomer = (Retailers)PkCustomer.SelectedItem;
        }

        private void PickerLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PkLocation.SelectedIndex == -1)
                return;
            var _Location = (Areas)PkLocation.SelectedItem;
        }


        public void BtnUpdateEdit_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);
                int i = -1;
                ActivitySubjects Subject = (ActivitySubjects)PkSubjectype.SelectedItem;
                ActivityActions ActiStatu = (ActivityActions)PkActiName.SelectedItem;
                ActivityTypes ActiType = (ActivityTypes)PkActTypeID.SelectedItem;
                ActivityPriority Priority = (ActivityPriority)PkPriority.SelectedItem;
                ActivityLStatus status = (ActivityLStatus)PkStatus.SelectedItem;
                Retailers Pcustomer = (Retailers)PkCustomer.SelectedItem;
                Areas location = (Areas)PkLocation.SelectedItem;

                oAct.ID = App.database.nextTableID("Activities");
                oAct.SubjectID = (Subject.ID);
                oAct.ActionID = (ActiStatu.ID);
                oAct.TypeID = (ActiType.ID);
                oAct.Details = textDetails.Text.ToString();
                oAct.StartDate = Convert.ToDateTime(CalStartDate.Date);
                TimeSpan interval = txtStartTime.Time;
                oAct.StartTime = interval;
                TimeSpan endtim = txtEndTime.Time;
                oAct.EndTime = endtim;
                oAct.EndDate = Convert.ToDateTime(CalEndtDate.Date);
                //  oAct.Priority = txtPriority.Text.Trim();
                oAct.Priority = (Priority.ID);
                oAct.BPName = (Pcustomer.Name);
                oAct.Status = status.ID == 0 ? true : false;
                oAct.Location = (location.ID);
                oAct.Notes = txtNotes.Text.Trim();
                if (Application.Current.Properties.ContainsKey("Notesphoto"))
                {
                    oAct.Attachment = UpdateImageinByte(Application.Current.Properties["Notesphoto"]);
                }
                else
                {
                    oAct.Attachment = null;
                }

                i = App.database.SaveNotes(oAct);
                Application.Current.Properties.Clear();

                if (i > 0)
                {
                    DisplayAlert("Save Notes ", "Your recods is submitted ", "OK");
                    Navigation.PopAsync();
                    return;
                }
                else
                {
                    DisplayAlert("Save Notes", "Update Fail .. Please try again ", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Application.Current.MainPage.DisplayAlert("Alert!", "Somthing went wrong.\nPlease try again later.", "OK");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
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
                    Application.Current.Properties["Notesphoto"] = photo;
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
                    Application.Current.Properties["Notesphoto"] = _MediaFile;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

    }
}
