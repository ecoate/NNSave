using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using Android.
namespace NNSave.Data
{
    public class RetrieveData
    {
        public const string dataServer = "http://private-722921-nnsave.apiary-mock.com/{1}";

        List<Location> GetAllLocations()
        {
            List<Location> locs = new List<Location>();

            string uri = string.Format(dataServer, "locations");

            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));

            try
            {

            }
            catch (Exception ex)
            {
                //Log.Debug(ex.Message);
            }

            return locs;
            
        }

    }
}
