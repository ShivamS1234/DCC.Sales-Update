namespace DCC.SalesApp.Models
{
    public class User:Default.Users
    {
        public string name
        {
            get { return FirstName + ' ' + MiddleName + ' ' + LastName; }
        }
        public string nameCode
        {
            get { return name + " (" + Code + ")"; }
        }

    }
}
