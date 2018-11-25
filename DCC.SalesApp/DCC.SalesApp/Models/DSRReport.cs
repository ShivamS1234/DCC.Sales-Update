using System;

namespace DCC.SalesApp.Models
{
    public class DSRReport
    {
        public string region { get; set; }
        public string custName { get; set; }
        public int orderNo { get; set; }
        public string Itemname { get; set; }
        public double Quantity { get; set; }
        public double DeliveredQty { get; set; }
        public double OpenQty { get; set; }
        public double Rate { get; set; }
        public double TotalAmount { get; set; }
        public int WhsCode { get; set; }
        public DateTime PostDate { get; set; }
        public int UserID { get; set; }
       

    }
}
