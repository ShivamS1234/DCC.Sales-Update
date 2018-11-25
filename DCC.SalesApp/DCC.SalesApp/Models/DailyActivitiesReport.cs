using System;

namespace DCC.SalesApp.Models
{
    public class DailyActivitiesReport
    {
        public string AreaCode { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
