using DCC.SalesApp.Pages;
using System;

namespace DCC.SalesApp.Menus
{

    public class MainPageMenuItem
    {
        public MainPageMenuItem()
        {
            TargetType = typeof(Dashboard);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }

    }
}