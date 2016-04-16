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
        const string dataServer = "http://private-722921-nnsave.apiary-mock.com/{0}";
        const string latlongstr = "locations/?latitude={0}&longitude={1}";
        const string idStr = "locations/{0}";
        const string baseStr = "locations";
        const string latlongdistanceStr = "locations/?latitude={0}&longitude={1}&distance={2}";
        const string latlongcountStr = "locations/?latitude={0}&longitude={1}&count={2}";
        const string latlngdistcountStr = "locations/?latitude={0}&longitude={1}&distance={2}&count={3}";

        #region Get all Locations

        public async Task<List<Location>> GetAllLocations()
        {
            try
            {
                string uri = string.Format(dataServer, baseStr);

                List<Location> locs = (await FetchListLocationsAsync(uri));


                return locs;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region view location detail
        public async Task<Location> GetLocationByID(int ID)
        {
            try
            {
                string endpoint = string.Format(idStr, ID);
                string uri = string.Format(dataServer, endpoint);

                Location locs = (await FetchLocationByIDAsync(uri));

                return locs;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private async Task<Location> FetchLocationByIDAsync(string uri)
        {
            Location loc = new Location();
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
            httpReq.ContentType = "application/json";
            httpReq.Method = "GET";

            using (WebResponse httpRes = await httpReq.GetResponseAsync())
            {
                using (var reader = new StreamReader(httpRes.GetResponseStream()))
                {
                    var str = reader.ReadToEnd();
                    loc = JsonConvert.DeserializeObject<Location>(str);

                }
            }

            return loc;

        }

        #endregion

        #region Get location with options
        
        public async Task<List<Location>> FindLocationByGeolocation(double latitude, double longitude, double distance = -1, int count=-1)
        {
            try
            {
                string endpoint = createURLEnd(latitude, longitude, distance, count);
                string uri = string.Format(dataServer, endpoint);

                List<Location> locs = (await FetchListLocationsAsync(uri));
                return locs;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region create new detected location

        public async Task<List<Location>> DetectLocation(double latitude, double longitude, double distance = -1, int count = -1)
        {
            try
            {
                string endpoint = createURLEnd(latitude, longitude, distance, count);
                string uri = string.Format(dataServer, endpoint);

                List<Location> locs = (await SendLocationAsync(uri));


                return locs;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        #endregion


        #region Get all Categories

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                string uri = string.Format(dataServer, baseStr);

                List<Category> cats = (await FetchListCategoriesAsync(uri));

                return cats;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region api calls
        private async Task<List<Category>> FetchListCategoriesAsync(string uri)
        {
            List<Category> categories = new List<Category>();
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
            httpReq.ContentType = "application/json";
            httpReq.Method = "GET";

            using (WebResponse httpRes = await httpReq.GetResponseAsync())
            {
                using (var reader = new StreamReader(httpRes.GetResponseStream()))
                {
                    var str = reader.ReadToEnd();
                    categories = JsonConvert.DeserializeObject<List<Category>>(str);

                }
            }

            return categories;

        }

        private async Task<List<Location>> SendLocationAsync(string uri)
        {
            List<Location> locList = new List<Location>();
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
            httpReq.ContentType = "application/json";
            httpReq.Method = "POST";

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

        private string createURLEnd(double latitude, double longitude, double distance = -1, int count = -1)
        {
            string retString = "";

            if (distance > 0 && count > 0)
            {
                retString = string.Format(latlngdistcountStr, latitude, longitude, distance, count);
            }
            else if (distance > 0)
            {
                retString = string.Format(latlongdistanceStr, latitude, longitude, distance);
            }
            else if (count > 0)
            {
                retString = string.Format(latlongcountStr, latitude, longitude, count);
            }
            else
            {
                retString =  string.Format(latlongstr, latitude, longitude);
            }

            return retString;
        }

        private async Task<List<Location>> FetchListLocationsAsync(string uri)
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

        #endregion
    }
}
