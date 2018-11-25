using System;
using System.IO;
using SQLite;
using DCC.SalesApp.iOS;
using DCC.SalesApp.Helpers;


[assembly: Xamarin.Forms.Dependency(typeof(IOS_SQLite))]

namespace DCC.SalesApp.iOS
{
    class IOS_SQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "DCCSalesApp.db3";
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var db = Path.Combine(DatabasePath(), sqliteFilename);
            return new SQLiteConnection( db, false);
        }

        public string DatabasePath()
        {
             
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string db = Path.Combine(docs);
            return db;
        }
    }
}
