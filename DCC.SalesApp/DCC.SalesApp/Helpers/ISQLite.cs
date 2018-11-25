using SQLite;

namespace DCC.SalesApp.Helpers
{

    public interface ISQLite
    {
        SQLiteConnection GetConnection();
        string DatabasePath();
    }

}
