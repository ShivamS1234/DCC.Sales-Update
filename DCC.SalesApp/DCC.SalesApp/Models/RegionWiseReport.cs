using System;

namespace DCC.SalesApp.Models
{
    public class RegionWiseReport
    {
        public string AreaCode { get; set; }
        public DateTime PostDate { get; set; }
        public string CustName { get; set; }
        public string CustCode { get; set; }
        public int NumberOfOrders { get; set; }
        public double RoundingAmnt { get; set; }
        public int UserId { get; set; }
    }
}
