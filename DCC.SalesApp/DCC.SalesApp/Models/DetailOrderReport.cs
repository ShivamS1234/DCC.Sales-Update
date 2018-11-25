using System;

namespace DCC.SalesApp.Models
{
    public class DetailOrderReport
    {
        public int UserID { get; set; }
        public int AreaID { get; set; }
        public string AreaCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SPerson { get { return FirstName + " " + MiddleName + " " + LastName; } }
        public string CustName { get; set; }
        public string ID { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal DeliveredQty { get; set; }
        public decimal OpenQty { get; set; }
        public decimal Rate { get; set;}
        public DateTime PostDate { get; set; }
    }
}
