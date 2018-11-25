using System;

namespace DCC.SalesApp.Models
{
    public class OrderFulfillNew
    {
        private string _CustName;
        private string _CustCode;        
        private string _AreaCode;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private bool _Flag;
        public string CustName {
            get
            {
                return _CustName;
            }
            set
            {
                _CustName = value;
            }

        }
        public string CustCode
        {
            get
            {
                return _CustCode;
            }
            set
            {
                _CustCode = value;
            }

        }
       
        public string CustNameCode
        {
            get
            {
                return _CustName + " (" + _CustCode + ")";
            }
        }
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }
        public string MiddleName
        {
            get
            {
                return _MiddleName;
            }
            set
            {
                _MiddleName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }

        public string AreaCode
        {
            get
            {
                return _AreaCode;
            }
            set
            {
                _AreaCode = value;
            }
        }

        public string NameLocation
        {
            get { return string.Format("{0} {1} {2}, {3}", FirstName, MiddleName,LastName, AreaCode); }
        }
        public bool Flag
        {
            get
            {
                return _Flag;
            }
            set
            {
                _Flag = value;
            }
        }
        public int ContPerson { get; set; }
        
        public int OrderId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime DueDate { get; set; }
        public double RoundingAmnt { get; set; }
        public string CGuid { get; set; }


    }
}
