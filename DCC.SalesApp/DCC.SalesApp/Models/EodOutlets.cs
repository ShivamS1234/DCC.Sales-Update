namespace DCC.SalesApp.Models
{

    public class EodOutlets
    {

        private int _ID;

        private string _Code;

        private string _Name;

        private bool _selected;

        private string _IsPJP;

        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        public string IsPJP
        {
            get
            {
                return _IsPJP;
            }
            set
            {
                _IsPJP = value;
            }
        }
    }

}
