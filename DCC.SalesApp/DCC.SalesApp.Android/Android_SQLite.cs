using DCC.SalesApp.Droid;
using Xamarin.Forms;
using DCC.SalesApp.Helpers;
using PCLStorage;
using SQLite;

[assembly: Dependency(typeof(Android_SQLite))]
namespace DCC.SalesApp.Droid
{
    class Android_SQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            SQLiteConnection sqlitConnection;
            var sqliteFilename = "DCCSalesApp.db3";
            string path = PortablePath.Combine(DatabasePath(), sqliteFilename);
            sqlitConnection = new SQLiteConnection(path, false);
            return sqlitConnection;

        }

        public string DatabasePath()
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            return folder.Path.ToString();
        }
    }
}