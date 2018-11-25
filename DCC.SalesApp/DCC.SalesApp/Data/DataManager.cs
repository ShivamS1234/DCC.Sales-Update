using System;
using System.Threading.Tasks;
using Default;
using System.Collections.Generic;
using DCC.SalesApp.Models;

namespace DCC.SalesApp.Data
{
    public class DataManager
    {
        IRestService restService;
        public DataManager(IRestService service)
        {
            restService = service;
        }
       
        public Task <Default.Users> RefreshUserAsync(String _email)
        {
            return restService.RefreshUserAsync(_email);
        }
        
        public Task<List<Models.SyncedTables>> RefreshTableRowsAsync(String tableName)
        {
            return restService.RefreshTableRowsAsync(tableName);
        }
        public Task<bool> AddUserSafety(UserSafety oUserSafety)
        {
            return restService.AddUserSafety(oUserSafety);
        }
        public Task<bool> AddUserEOD(Models.UserEOD oUserEOD)
        {
            return restService.AddUserEOD(oUserEOD);
        }

        public async Task<Models.UserEOD> GetUserEodAsync(string userid)
        {
            return await restService.GetUserEodAsync(userid);
        }
        public Task<bool> AddUserAttendance(UserAttendance oUserAttendance)
        {
            return restService.AddUserAttendance(oUserAttendance);
        }
    }
}
