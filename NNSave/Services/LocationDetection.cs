using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using NNSave.Data;
using Java.Lang;

namespace NNSave.Services
{
    public class LocationDetection
    {

        // Unique ID for our notification: 
        private static readonly int ButtonClickNotificationId = 1000;

        public static async void LocationChanged(Android.Locations.Location deviceLoc, Activity act)
        {
            var retriever = new RetrieveData();
            var deviceID = Android.OS.Build.Serial;
            if(deviceID == "unknown") { deviceID = "KTU84P1337UCUjfewelk245456"; }
            Data.Location nearbyLocation = await retriever.DetectLocation(deviceID, deviceLoc.Latitude, deviceLoc.Longitude);

            //foreach (var loc in nearbyLocations)
            //{
                Notification.Builder builder = new Notification.Builder(act)
                .SetContentTitle("Discount at " + nearbyLocation.name)
                .SetContentText(nearbyLocation.description)
                .SetAutoCancel(true)
                .SetSmallIcon(Resource.Drawable.Icon);

            Notification notification = builder.Build();
            NotificationManager notificationMgr = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            notificationMgr.Notify(ButtonClickNotificationId, builder.Build());
            //}

        }


    }
}