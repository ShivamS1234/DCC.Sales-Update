using System;

namespace DCC.SalesApp.Models
{
    public class UserEOD
    {
        [SQLite.PrimaryKey]
        [SQLite.AutoIncrement]
        public int id { get; set; }
        public int userid { get; set; }
        public DateTime eod_date { get; set; }
        public string call_made { get; set; }
        public string productive_call { get; set; }
        public string pjp_visited { get; set; }
        public string nonpjp_visited { get; set; }
    }
}
