using DCC.SalesApp.Helpers;
using Microsoft.Synchronization.ClientServices;
using PCLStorage;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DCC.SalesApp.Data
{
    public class ContextModelDefault
    {
        public static string SyncUri = "http://213.136.84.152/WebApplication/DefaultSyncService.svc/";
        private DefaultOfflineContext SyncContext { get; set; }
 
        public String DatabasePath { get; set; }
        public string DatabaseName { get; set; }

        public ContextModelDefault(string ID)
        {
            // SQLite Path

            //IFolder folder = FileSystem.Current.LocalStorage;
            this.DatabaseName = "DCCSalesApp.db3";
            ////this.DatabasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, this.DatabaseName);
            //this.DatabasePath = PortablePath.Combine(FileSystem.Current.LocalStorage.ToString(), this.DatabaseName);

            this.DatabasePath = Xamarin.Forms.DependencyService.Get<ISQLite>().DatabasePath();
            //// Context

            this.SyncContext = new DefaultOfflineContext(this.DatabaseName, new Uri(SyncUri, UriKind.Absolute));

            // Definition of the cache controller serialization format:
            this.SyncContext.CacheController.ControllerBehavior.SerializationFormat = SerializationFormat.ODataJSON;
            this.SyncContext.AddScopeParameters("UserId", ID);


        }

        public async Task<CacheRefreshStatistics> Sync()
        {
            try
            {
                var result = await this.SyncContext.SynchronizeAsync();
           

                if (result.Error != null)
                {
                    Debug.WriteLine(result.Error.Message);
                }

                return result;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}
