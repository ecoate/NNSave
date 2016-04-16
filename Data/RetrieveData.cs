using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using Android.
namespace NNSave.Data
{
    public class RetrieveData
    {
        public const string dataServer = "http://private-722921-nnsave.apiary-mock.com/{0}";

        public async Task<List<Location>> GetAllLocations()
        {
            try
            {
                string uri = string.Format(dataServer, "locations");

                List<Location> locs = (await FetchLocationsAsync(uri));


                return locs;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private async Task<List<Location>> FetchLocationsAsync(string uri)
        {
            List<Location> locList = new List<Location>();
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
            httpReq.ContentType = "application/json";
            httpReq.Method = "GET";

            using (WebResponse httpRes = await httpReq.GetResponseAsync())
            {
                using (var reader = new StreamReader(httpRes.GetResponseStream()))
                {
                    var str = reader.ReadToEnd();
                    locList = JsonConvert.DeserializeObject<List<Location>>(str);

                }
            }

            return locList;

        }

    }
}
