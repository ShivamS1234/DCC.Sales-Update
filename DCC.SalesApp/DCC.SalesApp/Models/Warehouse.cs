namespace DCC.SalesApp.Models
{
    public class Warehouse:Default.Warehouses
    {
        public string nameCode
        {
            get { return Name + ' ' + Code; }
        }
    }
}
