using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DCC.SalesApp.Models;
using DCC.SalesApp.Pages;
using Xamarin.Forms;

namespace DCC.SalesApp.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        public ICommand AddNewNoteCommand { protected set; get; }
        INavigation navigation;
        public ObservableCollection<NotesInfo> _NoteLists;
        public ObservableCollection<NotesInfo> NoteLists
        {
            get { return _NoteLists; }
            set
            {
                if (value != null)
                {
                    _NoteLists = value;
                    this.OnPropertyChanged("NoteLists");
                }
            }
        }
        public string subjectName;
        public string SubjectName
        {
            get { return subjectName; }
            set
            {
                if (value != null)
                {
                    subjectName = value;
                    this.OnPropertyChanged("SubjectName");
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
        public NotesViewModel(INavigation _navigation)
        {
            NoteLists = new ObservableCollection<NotesInfo>();
            navigation = _navigation;
            AddNewNoteCommand = new Command(OnAddNewNotes);
            GetNotesList();
            IsVisible = false;

        }

        public async void OnAddNewNotes()
        {
            try
            {
                navigation.PushAsync(new NoteNew() { Title = "Add New Note" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetNotesList()
        {
            List<NotesInfo> oNotesList = App.Database.GetNotes().ToList();
            if (oNotesList != null)
            {
                NoteLists.Clear();
                foreach (var cutomerData in oNotesList)
                {
                    NoteLists.Add(cutomerData);
                }
            }
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
                    SubjectName = oNotes.SubjectName;
                    //textActionName.Text = oNotes.ActivityName;
                    //textActivetype.Text = oNotes.TypeName;
                    //textDetails.Text = oNotes.Details;
                    //textStartDate.Text = thisDate.ToString("dd-MMM-yy");
                    //txtStartTime.Text = Convert.ToString(oNotes.StartTime);
                    //textEndtDate.Text = thisEndDate.ToString("dd-MMM-yy");
                    //txtEndTime.Text = Convert.ToString(oNotes.EndTime);
                    ////  txtPriority.Text = oNotes.Priority;
                    //if (oNotes.Priority == "H")
                    //{
                    //    txtPriority.Text = "High";
                    //}
                    //else if (oNotes.Priority == "M")
                    //{
                    //    txtPriority.Text = "Medium";
                    //}
                    //else if (oNotes.Priority == "L")
                    //{
                    //    txtPriority.Text = "Low";
                    //}
                    //txtLocation.Text = oNotes.Location;
                    //txtNotes.Text = oNotes.Notes;
                    //txtStatus.Text = oNotes.Status;
                    //if (oNotes.Status == "0")
                    //{
                    //    txtStatus.Text = "Yes";
                    //}
                    //else
                    //{
                    //    txtStatus.Text = "No";
                    //}
                    //Att_Image.Source = ImageSource.FromStream(() => new MemoryStream(oNotes.Attachment));
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
            }
        }

        #region ViewModle_NewNotes

        #endregion
    }
}