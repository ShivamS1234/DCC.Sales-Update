using System.IO;
using Xamarin.Forms;

namespace DCC.SalesApp.Models
{
    public class NotesInfo : BaseViewModel
    {
        private int _ID;
        private string _Details;
        private string _ActivityName;
        private string _SubjectName;
        private string _ActivityTypes;
        private string _TypeName;
        private string _Priority;
        private string _Location;
        private string _Notes;

        private System.Nullable<System.DateTime> _StartDate;
        private System.Nullable<System.TimeSpan> _StartTime;
        private System.Nullable<System.DateTime> _EndDate;
        private System.TimeSpan _EndTime;
        private string _Status;
        private byte[] _Attachment;


        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                // base.OnPropertyChanging("ID");
                _ID = value;
                // base.OnPropertyChanged("ID");
            }
        }

        public string BPCode { get; set; }
        public string Details
        {
            get
            {
                return _Details;
            }
            set
            {
                // base.OnPropertyChanging("Code");
                _Details = value;
                //  base.OnPropertyChanged("Code");
            }
        }

        public string ActivityName
        {
            get
            {
                return _ActivityName;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _ActivityName = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string SubjectName
        {
            get
            {
                return _SubjectName;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _SubjectName = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public bool _isNotesInfo = false;
        public bool IsNotesInfo
        {
            get { return _isNotesInfo; }
            set
            {
                if (value != null)
                {
                    _isNotesInfo = value;
                    this.OnPropertyChanged("IsNotesInfo");
                }
            }
        }

        public string ActivityTypes
        {
            get
            {
                return _ActivityTypes;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _ActivityTypes = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string TypeName
        {
            get
            {
                return _TypeName;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _TypeName = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _Priority = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _Location = value;
                //  base.OnPropertyChanged("Name");
            }
        }

        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _Notes = value;
                //  base.OnPropertyChanged("Name");
            }
        }
        public System.Nullable<System.DateTime> StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                //     base.OnPropertyChanging("CreatedDate");
                _StartDate = value;
                // base.OnPropertyChanged("CreatedDate");
            }
        }


        public System.Nullable<System.TimeSpan> StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                //  base.OnPropertyChanging("StartTime");
                _StartTime = value;
                //  base.OnPropertyChanged("StartTime");
            }
        }



        public System.Nullable<System.DateTime> EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                //     base.OnPropertyChanging("CreatedDate");
                _EndDate = value;
                // base.OnPropertyChanged("CreatedDate");
            }
        }

        public System.TimeSpan EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                // base.OnPropertyChanging("EndTime");
                _EndTime = value;
                //  base.OnPropertyChanged("EndTime");
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                //  base.OnPropertyChanging("Name");
                _Status = value;
                //  base.OnPropertyChanged("Name");
            }
        }


        public byte[] Attachment
        {
            get
            {
                return _Attachment;
            }
            set
            {
                // base.OnPropertyChanging("ProfileImg");
                _Attachment = value;
                // base.OnPropertyChanged("ProfileImg");
            }
        }

        public byte[] ItemImage
        {
            get
            {
                return _Attachment;
            }
            set
            {
                // base.OnPropertyChanging("ProfileImg");
                _Attachment = value;
                // base.OnPropertyChanged("ProfileImg");
            }
        }
        public Xamarin.Forms.ImageSource stream
        {
            get
            {
                return
                ImageSource.FromStream(() => new MemoryStream(_Attachment));
            }
        }

        public string StartDateEndDateTime
        {
            get { return string.Format("{0} ({1}) ({2}) ({3})", StartDate, StartTime, EndDate, EndTime); }
        }

        public string ActivitySubject
        {
            get { return string.Format("{0} ({1})", ActivityName, SubjectName); }
        }

        public string DetailStatus
        {
            get { return string.Format("{0} ({1})", Details, Status); }
        }



    }
}
