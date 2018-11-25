using System;

namespace DCC.SalesApp.Models
{
    public class Customer:Default.Retailers
    {
       
        public string nameCode
        {
            get
            {
                return Name + " (" + Code + ")";
            }
        }     
        
        public Int32 OverDueDays { get; set; }
                 
    }
    public class CustomerContact : Default.RetailerContacts
    {
       public int contId { get; set; }
        public string contactPerson
        {
           get
            {
                return FirstName + " " + MiddleName + " " + LastName+" ("+Phone1+")";
            }
        }
    }

    
     
}
