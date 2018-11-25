using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.Linq;
using DCC.SalesApp.Models;
using System.IO;
using DevExpress.Mobile.DataGrid.Theme;

namespace DCC.SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesDetail : ContentPage
    {
        int _oNoteId = 0;
        public NotesDetail(int _id)
        {
            InitializeComponent();
            _oNoteId = _id;
        }

        protected  override void OnAppearing()
        {
            base.OnAppearing();
            Theme.ApplyGridTheme();
            GetNotesById(_oNoteId);
            ThemeManager.RefreshTheme();
        }

        public void GetNotesById(int _NotesID)
        {
            try
            {
                List<NotesInfo> objwarehouse = App.Database.GetNotesOnly(_NotesID).ToList();
                foreach (NotesInfo oNotes in objwarehouse)
                {
                    DateTime thisDate = Convert.ToDateTime(oNotes.StartDate.Value.ToString());
                    DateTime thisEndDate = Convert.ToDateTime(oNotes.EndDate.Value.ToString());
                    textSubjectype.Text = oNotes.SubjectName;
                    textActionName.Text = oNotes.ActivityName;
                    textActivetype.Text = oNotes.TypeName;
                    textDetails.Text = oNotes.Details;
                    textStartDate.Text = thisDate.ToString("dd-MMM-yy");
                    txtStartTime.Text = Convert.ToString(oNotes.StartTime);
                    textEndtDate.Text = thisEndDate.ToString("dd-MMM-yy");
                    txtEndTime.Text = Convert.ToString(oNotes.EndTime);
                    //  txtPriority.Text = oNotes.Priority;
                    if (oNotes.Priority=="H")
                    {
                        txtPriority.Text = "High";
                    }
                    else if (oNotes.Priority == "M")
                    {
                        txtPriority.Text = "Medium";
                    }
                    else if (oNotes.Priority == "L")
                    {
                        txtPriority.Text = "Low";
                    }
                    txtLocation.Text = oNotes.Location;
                    txtNotes.Text = oNotes.Notes;
                    txtStatus.Text = oNotes.Status;
                    if (oNotes.Status == "0")
                    {
                        txtStatus.Text = "Yes";
                    }
                    else
                    {
                        txtStatus.Text = "No";
                    }

                    Att_Image.Source = ImageSource.FromStream(() => new MemoryStream(oNotes.Attachment));               
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


        }

    }
}