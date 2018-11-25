using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Default;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using DCC.SalesApp.Helpers;

namespace DCC.SalesApp.Data
{
    class RestService : IRestService
    {
        HttpClient client;

        public List<Users> Items { get; private set; }
        public Users oUser { get; private set; }
        public Models.UserEOD oEod { get; private set; }
        public RestService()
        {
            var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 25600000;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
        }

        public async Task<List<Users>> RefreshDataAsync()
        {
            Items = new List<Users>();
            var uri = new Uri(string.Format(Constants.RestUrl, "GetUsers"));
            try
            {
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Users>>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.InnerException.Message);

            }
            return Items;
        }




        public async Task<Users> RefreshUserAsync(String _email)
        {
            oUser = new Users();
            var uri = new Uri(string.Format(Constants.RestUrl + "user/" + _email + "/"));
            try
            {
                var response = await client.GetAsync(uri).ConfigureAwait(true);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    oUser = JsonConvert.DeserializeObject<Users>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                return oUser;

            }
            return oUser;

        }

        public async Task<List<Models.SyncedTables>> RefreshTableRowsAsync(String _tableName)
        {
            List<Models.SyncedTables> _SyncedTables = null;
            var uri = new Uri(string.Format(Constants.RestUrl, "GetTableRows/" + _tableName));
            try
            {
                var response = await client.GetAsync(uri).ConfigureAwait(true);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _SyncedTables = JsonConvert.DeserializeObject<List<Models.SyncedTables>>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                return _SyncedTables;

            }
            return _SyncedTables;

        }
        public async Task<bool> AddUserEOD(Models.UserEOD oUserEOD)
        {
            var uri = new Uri(string.Format(Constants.RestUrl, "UserEOD/"));
            try
            {
                var json = JsonConvert.SerializeObject(oUserEOD);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddUserSafety(UserSafety oUserSafety)
        {
            bool Isposted = false;

            var uri = new Uri(string.Format(Constants.RestUrl, "AddUserSafety"));
            try
            {
                var json = JsonConvert.SerializeObject(oUserSafety);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Isposted = true;
                }
            }
            catch (HttpRequestException ex)
            {
                return false;

            }
            return Isposted;

        }

        public async Task<bool> AddUserAttendance(UserAttendance oUserAttendance)
        {
            bool Isposted = false;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 25600000;

            var uri = new Uri(string.Format(Constants.RestUrl, "AddUserAttendance"));
            try
            {
                var json = JsonConvert.SerializeObject(oUserAttendance);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Isposted = true;
                }
            }
            catch (HttpRequestException ex)
            {
                return false;

            }
            return Isposted;

        }

        public async Task<Models.UserEOD> GetUserEodAsync(string userid)
        {
            oEod = new Models.UserEOD();
            var uri = new Uri(string.Format(Constants.RestUrl + "UserEOD/" + userid));
            try
            {
                var response = await client.GetAsync(uri).ConfigureAwait(true);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    oEod = JsonConvert.DeserializeObject<Models.UserEOD>(content);
                }
            }
            catch (HttpRequestException ex)
            {
                return oEod;

            }
            return oEod;

        }

    }
}
