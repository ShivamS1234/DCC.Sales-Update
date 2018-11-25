using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Default;

namespace DCC.SalesApp.Data
{
    public interface IRestService
    {
        Task<List<Users>> RefreshDataAsync();
        Task<Users> RefreshUserAsync(String _email);

        Task<List<Models.SyncedTables>> RefreshTableRowsAsync(String _tableName);
        Task<bool> AddUserSafety(UserSafety oUserSafety);


        Task<bool> AddUserEOD(Models.UserEOD oUserEOD);
        Task<bool> AddUserAttendance(UserAttendance oUserAttendance);
        Task <Models.UserEOD> GetUserEodAsync(string userid );
     
    }
}
