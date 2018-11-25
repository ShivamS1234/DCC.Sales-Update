namespace DCC.SalesApp.Models
{
    public   class EndOfDay
    {
        private System.Nullable<decimal> _PJPTOutlets;

        private System.Nullable<decimal> _PJPTVisited;

        private System.Nullable<decimal> _PJPTNvisited;

        private System.Nullable<decimal> _NPJPTOutlets;

        private System.Nullable<decimal> _NPJPTVisited;

        private System.Nullable<decimal> _NPJPTNvisited ;

        private System.Nullable<decimal> _AddOutlets ;

        private System.Nullable<decimal> _NotVisited ;

        private System.Nullable<decimal> _Productivity;


        public System.Nullable<decimal> PJPTOutlets 
        {
            get
            {
                return _PJPTOutlets;
            }
            set
            {
                _PJPTOutlets = value;
            }
        }

        public System.Nullable<decimal> PJPTVisited 
        {
            get
            {
                return _PJPTVisited;
            }
            set
            {
                _PJPTVisited = value;
            }
        }
        public System.Nullable<decimal> PJPTNvisited 
        {
            get
            {
                return _PJPTNvisited;
            }
            set
            {
                _PJPTNvisited = value;
            }
        }


        public System.Nullable<decimal> NPJPTOutlets
        {
            get
            {
                return _NPJPTOutlets;
            }
            set
            {
                _NPJPTOutlets = value;
            }
        }

        public System.Nullable<decimal> NPJPTVisited
        {
            get
            {
                return _NPJPTVisited;
            }
            set
            {
                _NPJPTVisited = value;
            }
        }
        public System.Nullable<decimal> NPJPTNvisited
        {
            get
            {
                return _NPJPTNvisited;
            }
            set
            {
                _NPJPTNvisited = value;
            }
        }


        public System.Nullable<decimal> AddOutlets 
        {
            get
            {
                return _AddOutlets;
            }
            set
            {
                _AddOutlets = value;
            }
        }

        public System.Nullable<decimal> NotVisited 
        {
            get
            {
                return _NotVisited;
            }
            set
            {
                _NotVisited = value;
            }
        }
        public System.Nullable<decimal> Productivity 
        {
            get
            {
                return _Productivity;
            }
            set
            {
                _Productivity = value;
            }
        }

        


    }
}
