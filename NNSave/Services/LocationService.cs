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
using Android.Util;

namespace NNSave.Services
{
    [Service]
    public class LocationService : Service, ILocationListener
    {

        IBinder binder;
        protected LocationManager locMgr = Android.App.Application.Context.GetSystemService("location") as LocationManager;
        readonly string logTag = "LocationService";
        public event EventHandler<LocationChangedEventArgs> LocationChanged = delegate { };
        public event EventHandler<ProviderDisabledEventArgs> ProviderDisabled = delegate { };
        public event EventHandler<ProviderEnabledEventArgs> ProviderEnabled = delegate { };
        public event EventHandler<StatusChangedEventArgs> StatusChanged = delegate { };



        public LocationService() { }

        public override IBinder OnBind(Intent intent)
        {
            binder = new LocationServiceBinder(this);
            return binder;
        }

        #region ILOcationListener stuff

        public void OnLocationChanged(Location location)
        {
            this.LocationChanged(this, new LocationChangedEventArgs(location));
            Log.Debug(logTag, String.Format("Latitude is {0}", location.Latitude));
            Log.Debug(logTag, String.Format("Longitude is {0}", location.Longitude));
            Log.Debug(logTag, String.Format("Altitude is {0}", location.Altitude));
            Log.Debug(logTag, String.Format("Speed is {0}", location.Speed));
            Log.Debug(logTag, String.Format("Accuracy is {0}", location.Accuracy));
            Log.Debug(logTag, String.Format("Bearing is {0}", location.Bearing));

        }

        public void OnProviderDisabled(string provider)
        {
            this.ProviderDisabled(this, new ProviderDisabledEventArgs(provider));
        }

        public void OnProviderEnabled(string provider)
        {
            this.ProviderEnabled(this, new ProviderEnabledEventArgs(provider));
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            this.StatusChanged(this, new StatusChangedEventArgs(provider, status, extras));
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }
        public override void OnDestroy()
        {
            // Stop receiving updates from the location manager:
            locMgr.RemoveUpdates(this);
        }

        #endregion

        public void StartLocationUpdates()
        {
            var locationCriteria = new Criteria();
            locationCriteria.Accuracy = Accuracy.Medium;
            locationCriteria.PowerRequirement = Power.NoRequirement;
            var locationProvider = locMgr.GetBestProvider(locationCriteria, true);
            locMgr.RequestLocationUpdates(locationProvider, 5000, 200, this); //TODO here is where we would pass parameters for distance/time?
        }

    }
}